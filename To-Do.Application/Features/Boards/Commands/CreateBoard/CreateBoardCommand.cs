using To_Do.Application.Abstractions.Messaging;

namespace To_Do.Application.Features.Boards.Commands.CreateBoard;

public sealed record CreateBoardCommand(string Name) : ICommand<CreateBoardResponse>;

