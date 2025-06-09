using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using To_Do.Application.Infrastructure.Validation;

namespace To_Do.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        services.AddScoped(typeof(Abstractions.IValidator<>), typeof(FluentValidator<>));

        return services;
    }
}