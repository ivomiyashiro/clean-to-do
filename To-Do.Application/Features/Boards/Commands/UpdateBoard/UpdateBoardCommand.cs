using To_Do.Application.Abstractions.Messaging;

namespace To_Do.Application.Features.Boards.Commands.UpdateBoard;

public sealed record UpdateBoardCommand(Guid Id, string Name) : ICommand<UpdateBoardResponse>;

