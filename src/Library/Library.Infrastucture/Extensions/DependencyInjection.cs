using Library.Domain.Interfaces;
using Library.Domain.Interfaces.Auth;
using Library.Domain.Interfaces.Repositories;
using Library.Infrastucture.Auth;
using Library.Infrastucture.Data;
using Library.Infrastucture.Data.Initializers;
using Library.Infrastucture.IdentityServer;
using Library.Infrastucture.Repositories.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Library.Infrastucture.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection InjectDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<LibraryDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            options.UseSqlServer(connectionString);
            options.UseLazyLoadingProxies();
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    public static IServiceCollection InjectRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(EntityFrameworkRepository<>));

        return services;
    }

    public static IServiceCollection InjectDbContextInitializers(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<DbContextInitializer>((provider) =>
        {
            var passwordHasher = provider.GetRequiredService<IPasswordHasher>();
            var unitOfWork = provider.GetRequiredService<IUnitOfWork>();

            return new DbContextInitializer(unitOfWork, passwordHasher);
        });

        return services;
    }

    public static IServiceCollection InjectAuthServices(this IServiceCollection services)
    {
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }

    public static IServiceCollection InjectJwtTokens(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("Jwt");
        var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
        });

        services.AddIdentityServer()
            .AddInMemoryClients(IdentityServerConfig.Clients)
            .AddInMemoryIdentityResources(IdentityServerConfig.IdentityResources)
            .AddInMemoryApiScopes(IdentityServerConfig.ApiScopes)
            .AddDeveloperSigningCredential();

        return services;
    }
}