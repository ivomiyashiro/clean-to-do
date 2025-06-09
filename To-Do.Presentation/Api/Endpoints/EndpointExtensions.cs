namespace To_Do.Presentation.Api.Endpoints;

public static class EndpointExtensions
{
    public static void MapApiEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapBoardEndpoints();
        // Add other endpoint mappings here as the API grows
    }
}