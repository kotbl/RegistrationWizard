using Microsoft.AspNetCore.Diagnostics;
using RegistrationWizard.Application;
using static System.Net.Mime.MediaTypeNames;
using System.Text.Json;
using RegistrationWizard.Server.Middlewares;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.DataAccessRegistration(configuration);
builder.Services.ApplicationRegistration();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();
    logging.AddLog4Net("log4net.config");
    logging.AddConsole();
});

var app = builder.Build();

InitializationDataBase.Update(app);

app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = Text.Plain;

        var logger = app.Services.GetService<ILogger<Program>>();
        var ex = context.Features.Get<IExceptionHandlerFeature>();

        if (ex != null)
        {
            await context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                error = ex.Error.Message.Replace("'", "")

            }));

            logger?.LogError(ex.Error.Message, ex);
        }
    });
});

app.UseMiddleware<ValidationExceptionHandlingMiddleware>();


app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
