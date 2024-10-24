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
        //.InjectIdentityServer(builder.Configuration)
        .InjectServices()
        .InjectValidators()
        .InjectAutoMapper()
        .AddCorsPolicy()
        //.AddPolicyBasedAuthorization()
        ;
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

    //app.UseStaticFiles();

    //app.UseRouting();

    app.UseHttpsRedirection();

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}