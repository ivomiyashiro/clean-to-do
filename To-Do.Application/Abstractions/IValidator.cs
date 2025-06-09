using To_Do.SharedKernel.Result;

namespace To_Do.Application.Abstractions;

public interface IValidator<in T>
{
    Task<Result> ValidateAsync(T instance, CancellationToken cancellationToken = default);
}