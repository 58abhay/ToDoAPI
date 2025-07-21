//using FluentValidation;
//using FluentValidation.AspNetCore;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Infrastructure;
//using ToDoAPI.API.Middleware;
//using ToDoAPI.Application.Interfaces;
//using ToDoAPI.Application.Services;
//using ToDoAPI.Application.Validators;
//using ToDoAPI.Infrastructure.Persistence;
//using ToDoAPI.Infrastructure.Persistence.Repositories;

//var builder = WebApplication.CreateBuilder(args);

//// Add EF Core with PostgreSQL
////builder.Services.AddDbContext<AppDbContext>(options =>
////    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
////                                            .MigrationsAssembly("ToDoAPI.Infrastructure"));
//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
//        sql => sql.MigrationsAssembly("ToDoAPI.Infrastructure")));

//// Add services
//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//// Register custom services
////builder.Services.AddSingleton<IToDoService, ToDoService>();
////builder.Services.AddSingleton<IUserService, UserService>();
////builder.Services.AddScoped<IUserService, UserService>();
////builder.Services.AddScoped<IToDoService, ToDoService>();
//builder.Services.AddScoped<IToDoRepository, ToDoRepository>();
//builder.Services.AddScoped<IToDoService, ToDoService>();
//builder.Services.AddScoped<IUserRepository, UserRepository>();
//builder.Services.AddScoped<IUserService, UserService>();

//// FluentValidation integration
//builder.Services.AddFluentValidationAutoValidation();
//builder.Services.AddValidatorsFromAssemblyContaining<CreateUserDtoValidator>();

//var app = builder.Build();

//// Add custom middleware for error handling
//app.UseMiddleware<ExceptionMiddleware>();

//// Swagger
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();
//app.UseAuthorization();
//app.MapControllers();
//app.Run();

//using FluentValidation;
//using FluentValidation.AspNetCore;
//using Microsoft.EntityFrameworkCore;
//using ToDoAPI.API.Middleware;
//using ToDoAPI.Application.Interfaces;
//using ToDoAPI.Application.Services;
//using ToDoAPI.Application.Validators;
//using ToDoAPI.Infrastructure.Persistence;

//var builder = WebApplication.CreateBuilder(args);

//// ✅ Add EF Core with PostgreSQL + Migrations from Infrastructure
//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
//           .MigrationsAssembly("ToDoAPI.Infrastructure"));

//// ✅ Add controllers + Swagger
//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//// ✅ Register application services (from Application project)
//builder.Services.AddScoped<IUserService, UserService>();
//builder.Services.AddScoped<IToDoService, ToDoService>();

//// ✅ FluentValidation setup (validators live in Application)
//builder.Services.AddFluentValidationAutoValidation();
//builder.Services.AddValidatorsFromAssemblyContaining<CreateUserDtoValidator>();

//var app = builder.Build();

//// ✅ Middleware pipeline
//app.UseMiddleware<ExceptionMiddleware>();

//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();
//app.UseAuthorization();
//app.MapControllers();

//app.Run();

using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using ToDoAPI.API.Middleware;
using ToDoAPI.Application.Interfaces;
using ToDoAPI.Application.Services;
using ToDoAPI.Application.Validators;
using ToDoAPI.Infrastructure.Persistence;
using ToDoAPI.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add PostgreSQL + EF Core
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sql => sql.MigrationsAssembly("ToDoAPI.Infrastructure")));

// Register Services & DI
builder.Services.AddScoped<IToDoRepository, ToDoRepository>();
builder.Services.AddScoped<IToDoService, ToDoService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

// Add Controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserDtoValidator>();

var app = builder.Build();

// Error Handling Middleware
app.UseMiddleware<ExceptionMiddleware>();

// Swagger (always on in dev)
app.UseSwagger();
app.UseSwaggerUI();


// Request Pipeline
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();