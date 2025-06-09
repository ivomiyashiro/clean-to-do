using To_Do.SharedKernel.Result;

namespace To_Do.Presentation.Api.Extensions;

public static class ErrorExtensions
{
    public static IResult ToIResult(this Error error)
    {
        return error.Type switch
        {
            ErrorType.Validation => Results.BadRequest(new { error = error.Message }),
            ErrorType.NotFound => Results.NotFound(new { error = error.Message }),
            ErrorType.Conflict => Results.Conflict(new { error = error.Message }),
            ErrorType.Unauthorized => Results.Unauthorized(),
            ErrorType.Failure => Results.BadRequest(new { error = error.Message }),
            _ => Results.Problem(detail: error.Message, statusCode: 500)
        };
    }
}