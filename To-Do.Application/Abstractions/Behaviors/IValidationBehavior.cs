using To_Do.SharedKernel.Result;

namespace To_Do.Application.Abstractions.Behaviors;

public interface IValidationBehavior<in TRequest, TResponse>
{
    Task<Result<TResponse>> ValidateAsync(TRequest request, CancellationToken cancellationToken);
}