using To_Do.Application.Abstractions.Messaging;
using To_Do.Domain.Respositories;
using To_Do.SharedKernel.Result;

namespace To_Do.Application.Features.Boards.Queries.GetAllBoards;

internal sealed class GetAllBoardsQueryHandler(IBoardRepository boardRepository) : IQueryHandler<GetAllBoardsQuery, GetAllBoardsResponse>
{
    private readonly IBoardRepository _boardRepository = boardRepository;

    public async Task<Result<GetAllBoardsResponse>> Handle(GetAllBoardsQuery request, CancellationToken cancellationToken)
    {
        var boards = await _boardRepository.GetAllBoardsAsync(cancellationToken);

        var boardDtos = boards.Select(b => new BoardDto(
            b.Id,
            b.Name,
            b.CreatedAt,
            b.UpdatedAt
        )).OrderBy(b => b.CreatedAt);

        var response = new GetAllBoardsResponse(boardDtos);
        return Result.Success(response);
    }
}