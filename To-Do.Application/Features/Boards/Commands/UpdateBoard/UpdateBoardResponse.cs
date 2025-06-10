using System;

namespace To_Do.Application.Features.Boards.Commands.UpdateBoard;

public sealed record UpdateBoardResponse(Guid Id, string Name, DateTime CreatedAt, DateTime UpdatedAt);
