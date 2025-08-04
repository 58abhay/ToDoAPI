using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using ToDoAPI.API.Middleware;
using ToDoAPI.Application.Configuration;
using ToDoAPI.Application.CQRS.AccountModule.Commands;
using ToDoAPI.Application.CQRS.TaskModule.Commands;
using ToDoAPI.Application.Interfaces;
using ToDoAPI.Application.Services;
using ToDoAPI.Application.Validators;
using ToDoAPI.Infrastructure.Persistence;
using ToDoAPI.Infrastructure.Persistence.Repositories;


namespace ToDoApi.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Serilog Configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development"}.json", optional: true)
                .Build();
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration) // Read Serilog config from appsettings.json
                .CreateLogger();
            Log.Information("Starting ToDo API web host.");
            var builder = WebApplication.CreateBuilder(args);
            // --- Serilog Integration with Host Builder
            builder.Host.UseSerilog();


//  EF Core + PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sql => sql.MigrationsAssembly("ToDoAPI.Infrastructure"))
);

//  MediatR (CQRS)
builder.Services.AddMediatR(
    typeof(CreateTaskItemCommand).Assembly,
    typeof(CreateAccountProfileCommand).Assembly
);

//  Repositories & Services
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();

//  FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateTaskDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateAccountDtoValidator>();

//  Configuration + AppSettings Validation
builder.Services.Configure<AppSettings>(
    builder.Configuration.GetSection("AppSettings"));

builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IOptions<AppSettings>>().Value
);

builder.Services.AddOptions<AppSettings>()
    .Bind(builder.Configuration.GetSection("AppSettings"))
    .ValidateDataAnnotations()
    .ValidateOnStart(); // Fail-fast if config is broken

//  Controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "ToDo API",
        Version = "v1",
        Description = "Endpoints for managing accounts and tasks",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Abhaydeep",
            Url = new Uri("https://www.linkedin.com/in/abhaydeep-singh-07328016b/")
        }
    });
});

var app = builder.Build();

//  Boot-Time Logging
var appSettings = app.Services.GetRequiredService<AppSettings>();
Log.Information("AppSettings loaded successfully: {@AppSettings}", appSettings);
Log.Information("Environment: {Environment}, ContentRoot: {ContentRoot}",
    builder.Environment.EnvironmentName, builder.Environment.ContentRootPath);

//  Global Middleware
app.UseMiddleware<ExceptionMiddleware>();

//  Dev Tools — Swagger
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDoAPI v1");
    options.RoutePrefix = "swagger";
});

//  Request Pipeline
if (!app.Environment.IsProduction())
{
app.UseHttpsRedirection();
}
app.UseSerilogRequestLogging(); // Serilog request logging
app.UseAuthorization();
app.MapControllers();

app.Run();
            }
    }
}

