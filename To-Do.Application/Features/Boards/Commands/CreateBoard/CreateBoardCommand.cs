using Mapster;
using To_Do.Application.Abstractions.Messaging;
using To_Do.Domain.Abstractions;
using To_Do.Domain.Entities;
using To_Do.Domain.Respositories;
using To_Do.SharedKernel.Result;

namespace To_Do.Application.Features.Boards.Commands.CreateBoard;

public sealed record CreateBoardCommand(string Name) : ICommand<CreateBoardResponse>;

internal sealed class CreateBoardCommandHandler(IBoardRepository boardRepository, IUnitOfWork unitOfWork) : ICommandHandler<CreateBoardCommand, CreateBoardResponse>
{
    private readonly IBoardRepository _boardRepository = boardRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<CreateBoardResponse>> Handle(CreateBoardCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Begin transaction
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            // Check if board with name already exists
            var existingBoard = await _boardRepository.GetBoardByNameAsync(request.Name, cancellationToken);

            if (existingBoard is not null)
            {
                return Result.Failure<CreateBoardResponse>(
                    Error.Conflict("BoardAlreadyExists", $"A board with name {request.Name} already exists")
                );
            }

            // Create a Board
            var board = new Board(request.Name);
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
