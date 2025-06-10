using MediatR;
using Microsoft.AspNetCore.Mvc;
using To_Do.Application.Features.Boards.Commands.CreateBoard;
using To_Do.Application.Features.Boards.Commands.UpdateBoard;
using To_Do.Presentation.Api.DTOs;
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

        group.MapPut("/{id}", UpdateBoard)
            .WithName("UpdateBoard")
            .WithSummary("Update a board")
            .WithDescription("Updates a board with the specified id and name. Board names must be unique and cannot exceed 50 characters.")
            .Produces<UpdateBoardResponse>(StatusCodes.Status200OK)
            .Produces<object>(StatusCodes.Status400BadRequest)
            .Produces<object>(StatusCodes.Status404NotFound)
            .Produces<object>(StatusCodes.Status409Conflict)
            .Produces<object>(StatusCodes.Status500InternalServerError);
    }

    private static async Task<IResult> CreateBoard(
        [FromBody] CreateBoardRequest request,
        [FromServices] IMediator _mediator,
        CancellationToken cancellationToken
    )
    {
        var command = new CreateBoardCommand(request);
        var result = await _mediator.Send(command, cancellationToken);

        return result.IsSuccess
            ? Results.Created($"/api/boards/{result.Value.Id}", result.Value)
            : result.Error.ToIResult();
    }

    private static async Task<IResult> UpdateBoard(
        [FromRoute] Guid id,
        [FromBody] UpdateBoardRequest request,
        [FromServices] IMediator _mediator,
        CancellationToken cancellationToken
    )
    {
        var command = new UpdateBoardCommand(id, request.Name);
        var result = await _mediator.Send(command, cancellationToken);

        return result.IsSuccess
            ? Results.Ok(result.Value)
            : result.Error.ToIResult();
    }
}