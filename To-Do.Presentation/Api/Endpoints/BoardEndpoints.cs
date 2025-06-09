using Microsoft.AspNetCore.Mvc;
using To_Do.Application.Abstractions.Messaging;
using To_Do.Application.Features.Boards.Commands.CreateBoard;
using To_Do.Presentation.Api.Extensions;

namespace To_Do.Presentation.Api.Endpoints;

public static class BoardEndpoints
{
    public static void MapBoardEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/boards")
            .WithTags("Boards");

        group.MapPost("/", CreateBoard)
            .WithName("CreateBoard")
            .WithSummary("Create a new board")
            .WithDescription("Creates a new board with the specified name. Board names must be unique and cannot exceed 50 characters.")
            .Produces<CreateBoardResponse>(StatusCodes.Status201Created)
            .Produces<object>(StatusCodes.Status400BadRequest)
            .Produces<object>(StatusCodes.Status409Conflict)
            .Produces<object>(StatusCodes.Status500InternalServerError);
    }

    private static async Task<IResult> CreateBoard(
        [FromBody] CreateBoardDTO dto,
        [FromServices] ICommandHandler<CreateBoardCommand, CreateBoardResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        var command = new CreateBoardCommand(dto);
        var result = await commandHandler.Handle(command, cancellationToken);

        return result.IsSuccess
            ? Results.Created($"/api/boards/{result.Value.Id}", result.Value)
            : result.Error.ToIResult();
    }
}