using FluentValidation;
using FluentValidation.AspNetCore;
using Library.Api.Middlewares;
using Library.Application.Interfaces;
using Library.Domain.Entities;
using Library.Infrastucture.Data;
using Library.Infrastucture.Data.Initializers;
using Library.Infrastucture.Repositories;
using Library.Presentation.Validators;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddDbContext<LibraryDbContext>(options =>
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        options.UseSqlServer(connectionString);
    });

    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

    builder.Services.AddScoped(typeof(IRepository<>), typeof(EntityFrameworkRepository<>));

    builder.Services.AddSingleton<IInitializer<Role>, RoleInitializer>();
    builder.Services.AddScoped<DbContextInitializer>((provider) =>
    {
        var roleInitializer = provider.GetRequiredService<IInitializer<Role>>();
        var initializer = new DbContextInitializer(roleInitializer);
        var unitOfWork = provider.GetRequiredService<IUnitOfWork>();

        initializer.Initialize(unitOfWork);
        return initializer;
    });

    builder.Services.AddFluentValidationAutoValidation();
    builder.Services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();
    builder.Services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();

    builder.Services.AddCors();

    builder.Services.AddAuthorizationBuilder()
        .AddPolicy("Admin", policy => policy.RequireRole("Admin"))
        .AddPolicy("User", policy => policy.RequireRole("User"));

}

var app = builder.Build();
{
    using var scope = app.Services.CreateScope();
    var initializer = scope.ServiceProvider.GetRequiredService<DbContextInitializer>();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

    app.UseCors();

    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}