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

//  PostgreSQL + EF Core
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sql => sql.MigrationsAssembly("ToDoAPI.Infrastructure"))
);

//  MediatR - CQRS Modules
builder.Services.AddMediatR(typeof(CreateTaskItemCommand).Assembly);
builder.Services.AddMediatR(typeof(CreateAccountProfileCommand).Assembly);

//  Repositories & Services
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();

//  FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateTaskDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateAccountDtoValidator>();

//  Controllers & Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//  Exception Middleware
app.UseMiddleware<ExceptionMiddleware>();

//  Dev Tools
app.UseSwagger();
app.UseSwaggerUI();

//  Request Pipeline
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();