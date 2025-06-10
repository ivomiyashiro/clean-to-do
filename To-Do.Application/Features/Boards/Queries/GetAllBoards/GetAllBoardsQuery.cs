using To_Do.Application.Abstractions.Messaging;

namespace To_Do.Application.Features.Boards.Queries.GetAllBoards;

public sealed record GetAllBoardsQuery : IQuery<GetAllBoardsResponse>;