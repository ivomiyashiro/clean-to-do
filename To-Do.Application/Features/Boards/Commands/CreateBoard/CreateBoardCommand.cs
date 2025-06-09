using FluentValidation;
using Mapster;
using To_Do.Application.Abstractions.Messaging;
using To_Do.Domain.Abstractions;
using To_Do.Domain.Entities;
using To_Do.Domain.Respositories;
using To_Do.SharedKernel.Result;

namespace To_Do.Application.Features.Boards.Commands.CreateBoard;

public record CreateBoardDTO(string Name);

public sealed record CreateBoardCommand(CreateBoardDTO DTO) : ICommand<CreateBoardResponse>;

internal sealed class CreateBoardCommandHandler(
    IValidator<CreateBoardDTO> validator,
    IBoardRepository boardRepository,
    IUnitOfWork unitOfWork
) : ICommandHandler<CreateBoardCommand, CreateBoardResponse>
{
    private readonly IValidator<CreateBoardDTO> _validator = validator;
    private readonly IBoardRepository _boardRepository = boardRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<CreateBoardResponse>> Handle(CreateBoardCommand request, CancellationToken cancellationToken)
    {
        // Validate command input with FluentValidation
        var validationResult = await _validator.ValidateAsync(request.DTO, cancellationToken);

        if (!validationResult.IsValid)
        {
            return Result.Failure<CreateBoardResponse>(
                Error.Validation(
                    "ValidationFailed",
                    string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage))
                )
            );
        }

        // Check if board with name = name already exists
        var existingBoard = await _boardRepository.GetBoardByNameAsync(request.DTO.Name, cancellationToken);

        if (existingBoard is not null)
        {
            return Result.Failure<CreateBoardResponse>(
                Error.Conflict("BoardAlreadyExists", "A board with this name already exists")
            );
        }

        try
        {
            // Begin transaction
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            // Create a Board
            var board = new Board(request.DTO.Name);
            var createdBoard = await _boardRepository.CreateBoardAsync(board, cancellationToken);

            // Commit transaction
            await _unitOfWork.CommitTransactionAsync(cancellationToken);

            // Return success with the Board
            var response = createdBoard.Adapt<CreateBoardResponse>();

            return Result.Success(response);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            return Result.Failure<CreateBoardResponse>(
                Error.Failure("CreateBoardFailed", $"Failed to create board: {ex.Message}")
            );
        }
    }
}
