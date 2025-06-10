namespace To_Do.Application.Features.Boards.Queries.GetAllBoards;

public sealed record BoardDto(Guid Id, string Name, DateTime CreatedAt, DateTime UpdatedAt);

public sealed record GetAllBoardsResponse(IEnumerable<BoardDto> Boards);