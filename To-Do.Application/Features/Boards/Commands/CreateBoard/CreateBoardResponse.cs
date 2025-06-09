namespace To_Do.Application.Features.Boards.Commands.CreateBoard;

public sealed record CreateBoardResponse(Guid Id, string Name, DateTime CreatedAt, DateTime UpdatedAt);