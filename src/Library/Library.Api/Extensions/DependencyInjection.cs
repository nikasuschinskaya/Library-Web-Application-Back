using Library.Domain.Enums;
using Library.Domain.Enums.Extentions;

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

    public static IServiceCollection AddPolicyBasedAuthorization(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
            .AddPolicy("Admin", policy => policy.RequireRole("Admin"))
            .AddPolicy("User", policy => policy.RequireRole("User"));
        //services.AddAuthorizationBuilder()
        //    .AddPolicy(Roles.Admin.StringValue(), policy => policy.RequireRole(Roles.Admin.StringValue()))
        //    .AddPolicy(Roles.User.StringValue(), policy => policy.RequireRole(Roles.User.StringValue()));

        return services;
    }
}
