using To_Do.Application.Abstractions;
using To_Do.Application.Abstractions.Behaviors;
using To_Do.SharedKernel.Result;

namespace To_Do.Application.Infrastructure.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse>(IValidator<TRequest>? validator = null) : IValidationBehavior<TRequest, TResponse>
{
    private readonly IValidator<TRequest>? _validator = validator;

    public async Task<Result<TResponse>> ValidateAsync(TRequest request, CancellationToken cancellationToken)
    {
        if (_validator is null)
        {
            return Result.Success(default(TResponse)!);
        }

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsFailure)
        {
            return Result.Failure<TResponse>(validationResult.Error);
        }

        return Result.Success(default(TResponse)!);
    }
}