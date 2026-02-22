using CodeEditor.Api.Extensions;
using CodeEditor.Domain.Requests.AuthRequests.Validators;
using FluentValidation;
using Microsoft.OpenApi;
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

builder.Services.AddControllers()
     .AddJsonOptions(options =>
     {
         options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
     });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddOpenApi((options) =>
{
    options.OpenApiVersion = OpenApiSpecVersion.OpenApi3_0;
    options.AddSchemaTransformer((schema, context, cancellationToken) =>
    {
        // Map long to int64
        if (context.JsonTypeInfo.Type == typeof(long) ||
            context.JsonTypeInfo.Type == typeof(long?))
        {
            schema.Type = JsonSchemaType.Integer;
            schema.Format = "int64";
        }
        return Task.CompletedTask;
    });
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

app.Run();
