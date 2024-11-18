using FluentValidation;
using FluentValidation.AspNetCore;
using Library.Application.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Library.Presentation.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection InjectAutoMapper(this IServiceCollection services)
    {
        services.InjectApplicationMapper();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }

    public static IServiceCollection InjectValidators(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}
