# 📝 ToDoAPI - RESTful Web API for Task & User Management

A fully documented and production-grade Web API built with ASP.NET Core. Includes clean architecture, user and task CRUD operations, input validation with FluentValidation, and structured error handling.

---

## 🚀 Features

- ✅ Create, Read, Update, Delete for Users and Tasks  
- ✅ Custom response schema using `ApiResponse<T>`  
- ✅ FluentValidation for input validation  
- ✅ Data annotations for model validation  
- ✅ Exception handling middleware for clean error reporting  
- ✅ Swagger UI for API exploration and testing  
- ✅ Organized project architecture with separate folders for DTOs, Models, Services, Interfaces, Validators, and Middleware  
- ✅ In-memory data storage for simplicity (singleton services)

---

## 🏗 Project Architecture
```ToDoAPI/ ├── Controllers/ │   ├── TasksController.cs │   └── UserController.cs ├── Middleware/ │   └── ExceptionMiddleware.cs ├── Models/ │   ├── ApiResponse.cs │   ├── ToDo.cs │   └── User.cs ├── Models/DTOs/ │   ├── CreateToDoDto.cs │   ├── CreateUserDto.cs │   ├── UpdateToDoDto.cs │   └── UpdateUserDto.cs ├── Services/ │   ├── ToDoService.cs │   └── UserService.cs ├── Services/Interfaces/ │   ├── IToDoService.cs │   └── IUserService.cs ├── Validators/ │   ├── CreateToDoDtoValidator.cs │   ├── UpdateToDoDtoValidator.cs │   ├── CreateUserDtoValidator.cs │   └── UpdateUserDtoValidator.cs ├── Program.cs └── README.md```

---

## 🛠 Technologies Used

- ASP.NET Core Web API  
- FluentValidation  
- Swagger (Swashbuckle)  
- Visual Studio 2022+  
- .NET 6 or 7 SDK  

---

## 🧪 API Endpoints Overview

### Tasks

| Method | Endpoint            | Description             |
|--------|---------------------|-------------------------|
| GET    | `/api/tasks`        | Retrieve all tasks      |
| GET    | `/api/tasks/{id}`   | Retrieve a task by ID   |
| POST   | `/api/tasks`        | Create a new task       |
| PUT    | `/api/tasks/{id}`   | Update a task           |
| DELETE | `/api/tasks/{id}`   | Delete a task           |

### Users

| Method | Endpoint            | Description             |
|--------|---------------------|-------------------------|
| GET    | `/api/user`         | Retrieve all users      |
| GET    | `/api/user/{id}`    | Retrieve a user by ID   |
| POST   | `/api/user`         | Create a new user       |
| PUT    | `/api/user/{id}`    | Update a user           |
| DELETE | `/api/user/{id}`    | Delete a user           |

---

## 🔐 Response Format

All endpoints return a standardized response in JSON format using the generic `ApiResponse<T>` wrapper:

```json
{
  "success": true,
  "message": "Descriptive status message",
  "data": { /* Object or list */ }
}
```
- success: Boolean status of the operation
- message: Human-readable outcome
- data: Actual resource or collection

---

## ✅ Validation

- Input validation is handled via FluentValidation.
- All DTOs are annotated with DataAnnotations such as `[Required]`, `[EmailAddress]`, and `[MinLength]`.
- Fluent validators are registered using:

```builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserDtoValidator>();
```
- Invalid input triggers early model validation with descriptive error messages.

---

## ⚠️ Error Handling

- Custom middleware intercepts unhandled exceptions.
- Errors are logged and returned using a clean JSON format:

```{
  "message": "An unexpected error occurred."
}
```
- This prevents exposure of internal stack traces to the client.

---

## 🧰 Developer Notes

- Uses in-memory data storage via `List<T>`
- Services registered as Singleton to retain state across requests
- API structure follows clean architecture best practices
- Swagger UI available at `/swagger` for live testing
- FluentValidation ensures reliable and maintainable input rules
- ExceptionMiddleware improves observability and debugging

---

## 📚 Future Enhancements

- JWT Authentication with role-based authorization
- Persistent database with Entity Framework Core
- Custom logging middleware with request tracing
- Unit and integration testing via xUnit
- API versioning and rate limiting
- OpenAPI enhancements with XML docs and schema examples

