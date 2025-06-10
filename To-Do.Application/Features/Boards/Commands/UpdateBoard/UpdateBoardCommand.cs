using Mapster;
using To_Do.Application.Abstractions.Messaging;
using To_Do.Domain.Abstractions;
using To_Do.Domain.Respositories;
using To_Do.SharedKernel.Result;

namespace To_Do.Application.Features.Boards.Commands.UpdateBoard;

public sealed record UpdateBoardCommand(Guid Id, string Name) : ICommand<UpdateBoardResponse>;

internal sealed class UpdateBoardCommandHandler(IBoardRepository boardRepository, IUnitOfWork unitOfWork) : ICommandHandler<UpdateBoardCommand, UpdateBoardResponse>
{
    private readonly IBoardRepository _boardRepository = boardRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<UpdateBoardResponse>> Handle(UpdateBoardCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Begin transaction
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            // Check if board with id exists
            var boardToUpdate = await _boardRepository.GetBoardByIdAsync(request.Id, cancellationToken);

            if (boardToUpdate is null)
            {
                return Result.Failure<UpdateBoardResponse>(
                    Error.NotFound("BoardNotFound", $"Board with id {request.Id} not found")
                );
            }

            var existingBoardWithSameName = await _boardRepository.GetBoardByNameAsync(request.Name, cancellationToken);

            if (existingBoardWithSameName is not null)
            {
                return Result.Failure<UpdateBoardResponse>(
                    Error.Conflict("BoardAlreadyExists", $"Board with name {request.Name} already exists")
                );
            }

            boardToUpdate.Update(request.Name);
            var updatedBoard = _boardRepository.UpdateBoard(boardToUpdate);

            // Commit transaction
            await _unitOfWork.CommitTransactionAsync(cancellationToken);

            // Return success with the Board
            var response = updatedBoard.Adapt<UpdateBoardResponse>();

            return Result.Success(response);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            return Result.Failure<UpdateBoardResponse>(
                Error.Failure("UpdateBoardFailed", $"Failed to update board: {ex.Message}")
            );
        }
    }
}
