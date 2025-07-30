using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoAPI.API.Middleware;
using ToDoAPI.Application.CQRS.AccountModule.Commands;
using ToDoAPI.Application.CQRS.TaskModule.Commands;
using ToDoAPI.Application.Interfaces;
using ToDoAPI.Application.Services;
using ToDoAPI.Application.Validators;
using ToDoAPI.Infrastructure.Persistence;
using ToDoAPI.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Database: PostgreSQL + EF Core
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sql => sql.MigrationsAssembly("ToDoAPI.Infrastructure"))
);

// MediatR (CQRS)
builder.Services.AddMediatR(
    typeof(CreateTaskItemCommand).Assembly,
    typeof(CreateAccountProfileCommand).Assembly
);

// Repositories & Services
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();

// FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateTaskDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateAccountDtoValidator>();

// Controllers & Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
<<<<<<< Updated upstream
builder.Services.AddSwaggerGen();
=======
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
>>>>>>> Stashed changes

var app = builder.Build();

// Global Exception Handler
app.UseMiddleware<ExceptionMiddleware>();

// Dev Tools
app.UseSwagger();
app.UseSwaggerUI();

<<<<<<< Updated upstream
// Request Pipeline
app.UseHttpsRedirection();
=======

//  Request Pipeline
//app.UseHttpsRedirection();

if (!app.Environment.IsProduction())
{
    app.UseHttpsRedirection();
}

app.UseSerilogRequestLogging(); // Serilog request logging
>>>>>>> Stashed changes
app.UseAuthorization();
app.MapControllers();

app.Run();