using MediatR;
using Microsoft.Extensions.DependencyInjection;
using To_Do.Application.Abstractions;
using To_Do.SharedKernel.Result;

namespace To_Do.Application.Infrastructure.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IServiceProvider serviceProvider) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var validator = _serviceProvider.GetService<IValidator<TRequest>>();

        if (validator is null)
        {
            return await next();
        }

        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsFailure)
        {
            // Handle Result<T> responses
            if (typeof(TResponse).IsGenericType && typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
            {
                var resultType = typeof(TResponse).GetGenericArguments()[0];
                var failureMethod = typeof(Result<>).MakeGenericType(resultType).GetMethod("Failure", [typeof(Error)]);
                if (failureMethod != null)
                {
                    return (TResponse)failureMethod.Invoke(null, [validationResult.Error])!;
                }
            }
            // Handle Result responses
            else if (typeof(TResponse) == typeof(Result))
            {
                return (TResponse)(object)Result.Failure(validationResult.Error);
            }
        }

        return await next();
    }
}