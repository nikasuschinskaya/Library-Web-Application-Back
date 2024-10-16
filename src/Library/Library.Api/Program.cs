using Library.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddAuthorizationBuilder()
        .AddPolicy("Admin", policy => policy.RequireRole("Admin"))
        .AddPolicy("User", policy => policy.RequireRole("User"));
}


var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

    app.UseCors();
    //app.UseCors(builder =>
    //{
    //    builder.WithOrigins("http://localhost:5173")
    //           .AllowAnyHeader()
    //           .AllowAnyMethod()
    //           .AllowCredentials();
    //});

    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
