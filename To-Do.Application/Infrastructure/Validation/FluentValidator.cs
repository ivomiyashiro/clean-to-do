using FluentValidation;
using To_Do.SharedKernel.Result;

namespace To_Do.Application.Infrastructure.Validation;

public sealed class FluentValidator<T>(IValidator<T> validator) : Abstractions.IValidator<T>
{
    private readonly IValidator<T> _validator = validator;

    public async Task<Result> ValidateAsync(T instance, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(instance, cancellationToken);

        if (validationResult.IsValid)
        {
            return Result.Success();
        }

        var errorMessage = string.Join(", ", validationResult.Errors
            .Select(error => $"{error.PropertyName}: {error.ErrorMessage}"));

        return Result.Failure(Error.Validation("ValidationFailed", errorMessage));
    }
}