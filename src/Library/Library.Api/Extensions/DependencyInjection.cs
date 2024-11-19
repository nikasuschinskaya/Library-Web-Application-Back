namespace Library.Api.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });
        });

        return services;
    }

    public static IServiceCollection AddRolesPolicy(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
            .AddPolicy("Admin", policy => policy.RequireRole("Admin"))
            .AddPolicy("User", policy => policy.RequireRole("User"));

        return services;
    }
}
