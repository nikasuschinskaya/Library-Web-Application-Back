using Library.Application.Interfaces;
using Library.Domain.Entities;
using Library.Infrastucture.Data;
using Library.Infrastucture.Data.Initializers;
using Library.Infrastucture.IdentityServer;
using Library.Infrastucture.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
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
        services.AddSingleton<IInitializer<Role>, RoleInitializer>();

        services.AddScoped<DbContextInitializer>((provider) =>
        {
            var roleInitializer = provider.GetRequiredService<IInitializer<Role>>();
            var initializer = new DbContextInitializer(roleInitializer);
            var unitOfWork = provider.GetRequiredService<IUnitOfWork>();

            initializer.Initialize(unitOfWork);
            return initializer;
        });

        return services;
    }

    //public static IServiceCollection InjectIdentityServer(this IServiceCollection services, IConfiguration configuration)
    //{
    //    services.AddIdentity<User, IdentityRole>()
    //       .AddEntityFrameworkStores<LibraryDbContext>() 
    //       .AddDefaultTokenProviders();

    //    services.AddIdentityServer(options =>
    //    {
    //        options.Events.RaiseSuccessEvents = true;
    //        options.Events.RaiseFailureEvents = true;
    //        options.Events.RaiseErrorEvents = true;
    //    })
    //    .AddInMemoryApiScopes(IdentityServerConfig.ApiScopes) 
    //    .AddInMemoryClients(IdentityServerConfig.Clients)     
    //    .AddInMemoryIdentityResources(IdentityServerConfig.IdentityResources) 
    //    .AddAspNetIdentity<User>() 
    //    .AddDeveloperSigningCredential(); 

     
    //    var jwtSettings = configuration.GetSection("Jwt");
    //    var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);
    //    services.AddAuthentication(options =>
    //    {
    //        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    //        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    //    })
    //    .AddJwtBearer(options =>
    //    {
    //        options.TokenValidationParameters = new TokenValidationParameters
    //        {
    //            ValidateIssuer = true,
    //            ValidateAudience = true,
    //            ValidateLifetime = true,
    //            ValidateIssuerSigningKey = true,
    //            ValidIssuer = jwtSettings["Issuer"],
    //            ValidAudience = jwtSettings["Audience"],
    //            IssuerSigningKey = new SymmetricSecurityKey(key)
    //        };
    //    });

      
    //    services.AddIdentityServer()
    //        .AddAspNetIdentity<User>(); 

    //    return services;
    //}

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