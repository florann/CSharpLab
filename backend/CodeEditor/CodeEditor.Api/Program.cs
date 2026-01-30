using CodeEditor.Api.Extensions;
using CodeEditor.Api.Hubs;
using CodeEditor.Domain.Requests.AuthRequests.Validators;
using FluentValidation;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

builder.AddConfiguration();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();

builder.Logging.AddConsole();

builder.Logging.SetMinimumLevel(LogLevel.Debug);

builder.Host.UseSerilog();

builder.Services.AddServices();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddOpenApi((options) =>
{
    options.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi3_0;
});

var app = builder.Build();

app.UseCors("AllowAngular");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
else if (app.Environment.IsProduction())
{
    app.UseHttpsRedirection();
}

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>("/chathub");

app.MapHub<CursorHub>("/cursorhub");

app.Run();
