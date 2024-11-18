using Library.Application.Interfaces.UseCases;
using Library.Application.Mappers.Profiles;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Application.Extensions;

public static class DependencyInjection
{
    //public static IServiceCollection InjectServices(this IServiceCollection services)
    //{

    //    services.AddScoped<IBookService, BookService>();
    //    services.AddScoped<IAuthorService, AuthorService>();
    //    services.AddScoped<IGenreService, GenreService>();
    //    services.AddScoped<IUserService, UserService>();

    //    return services;
    //}

    public static IServiceCollection InjectUseCases(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssemblyOf<IUseCase>()
            .AddClasses(classes => classes.AssignableTo<IUseCase>())
            .AsImplementedInterfaces()
            .WithScopedLifetime());


        return services;
    }

    public static IServiceCollection InjectApplicationMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(BookEntityProfile));
        return services;
    }
}
