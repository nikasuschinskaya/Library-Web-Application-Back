using Library.Api.Extensions;
using Library.Api.Middlewares;
using Library.Application.Extensions;
using Library.Infrastucture.Data.Initializers;
using Library.Infrastucture.Extensions;
using Library.Presentation.Extensions;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers()
         .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services
        .InjectDbContext(builder.Configuration)
        .InjectRepositories()
        .InjectDbContextInitializers()
        .InjectJwtTokens(builder.Configuration)
        .InjectFileStorage(builder.Environment)
        .InjectAuthServices()
        .InjectUseCases()
        .InjectValidators()
        .InjectAutoMapper()
        .AddCorsPolicy()
        .AddRolesPolicy()
        ;
}

var app = builder.Build();
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<DbContextInitializer>();
        await dbInitializer.InitializeAsync();
    }

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

    app.UseCors();

    app.UseStaticFiles();

    app.UseHttpsRedirection();

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}