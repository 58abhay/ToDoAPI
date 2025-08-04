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
```
ToDoAPI/ (Solution)
├── test/
│   └── ToDoAPI.UnitTests/
│       ├── Controllers/
│       │   └── TaskItemsControllerTests.cs
│       └── Handlers/
│           ├── CreateTaskItemHandlerTests.cs
│           └── GetTaskItemByIdHandlerTests.cs
├── docker-compose/
│   └── docker-compose.yml
├── ToDoAPI.API/
│   ├── Controllers/
│   │   ├── AccountProfilesController.cs
│   │   └── TaskItemsController.cs
│   ├── Middleware/
│   │   └── ExceptionMiddleware.cs
│   ├── Docker/
│   │   └── .dockerignore
│   └── Program.cs
├── ToDoAPI.Application/
│   ├── Configuration/
│   │   └── AppSettings.cs
│   ├── CQRS/
│   │   ├── AccountModule/
│   │   │   ├── Commands/
│   │   │   │   ├── CreateAccountProfileCommand.cs
│   │   │   │   ├── DeleteAccountProfileCommand.cs
│   │   │   │   └── UpdateAccountProfileCommand.cs
│   │   │   ├── Handler/
│   │   │   │   ├── CreateAccountProfileHandler.cs
│   │   │   │   ├── DeleteAccountProfileHandler.cs
│   │   │   │   ├── GetAccountProfileByIdHandler.cs
│   │   │   │   ├── GetAccountProfileListHandler.cs
│   │   │   │   └── UpdateAccountProfileHandler.cs
│   │   │   └── Queries/
│   │   │       ├── GetAccountProfileByIdQuery.cs
│   │   │       └── GetAccountProfileListQuery.cs
│   │   └── TaskModule/
│   │       ├── Commands/
│   │       │   ├── CreateTaskItemCommand.cs
│   │       │   ├── DeleteTaskItemCommand.cs
│   │       │   └── UpdateTaskItemCommand.cs
│   │       ├── Handler/
│   │       │   ├── CreateTaskItemHandler.cs
│   │       │   ├── DeleteTaskItemHandler.cs
│   │       │   ├── GetTaskItemByIdHandler.cs
│   │       │   ├── GetTaskItemListHandler.cs
│   │       │   └── UpdateTaskItemHandler.cs
│   │       └── Queries/
│   │           ├── GetTaskItemByIdQuery.cs
│   │           └── GetTaskItemListQuery.cs
│   ├── DTOs/
│   │   ├── CreateAccountDto.cs
│   │   ├── CreateTaskDto.cs
│   │   ├── TaskDto.cs
│   │   ├── UpdateAccountDto.cs
│   │   └── UpdateTaskDto.cs
│   ├── Interface/
│   │   ├── IAccountRepository.cs
│   │   ├── IAccountService.cs
│   │   ├── ITaskRepository.cs
│   │   └── ITaskService.cs
│   ├── Services/
│   │   ├── AccountService.cs
│   │   └── TaskService.cs
│   └── Validation/
│       ├── CreateAccountDtoValidator.cs
│       ├── CreateTaskDtoValidator.cs
│       ├── UpdateAccountDtoValidator.cs
│       └── UpdateTaskDtoValidator.cs
├── ToDoAPI.Domain/
│   ├── Entities/
│   │   ├── AccountProfile.cs
│   │   ├── ApiResponse.cs
│   │   └── TaskItem.cs
│   └── Exception/
│       ├── NotFoundException.cs
│       └── ValidationException.cs
├── ToDoAPI.Infrastructure/
│   ├── Migrations (Auto-generated)
│   └── Persistence/
│       ├── Repositories/
│       │   ├── AccountRepository.cs
│       │   └── TaskRepository.cs
│       ├── AppDbContext.cs
│       └── DesignTimeDbContextFactory.cs
└── README.md
```

---

## 🛠 Technologies Used

- ASP.NET Core Web API  
- FluentValidation  
- Swagger (Swashbuckle)  
- Visual Studio 2022+  
- .NET 6 or 7 SDK or SDK 8  

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

```
json
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

```
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserDtoValidator>();
```
- Invalid input triggers early model validation with descriptive error messages.

---

## ⚠️ Error Handling

- Custom middleware intercepts unhandled exceptions.
- Errors are logged and returned using a clean JSON format:

```
{
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

## 🗄️ Database Integration with PostgreSQL

This project now uses Entity Framework Core (EF Core) to connect to a real PostgreSQL database, replacing the previous in-memory storage. All data is persisted across sessions, enabling production-grade reliability and scalability.

### ✅ Highlights

- 🔗 Connected to PostgreSQL using `Npgsql.EntityFrameworkCore.PostgreSQL`
- 🧠 Replaced in-memory `List<T>` logic with EF Core queries
- 📦 Data is stored in `ToDoApiDb` with tables for `Users` and `ToDos`
- 🛠️ Migrations are managed via EF CLI (`dotnet ef`)
- 🔄 Services now use `AppDbContext` for all CRUD operations

### 📁 Key Changes

- Created `AppDbContext.cs` in /Data folder
- Registered EF Core in `Program.cs` using `AddDbContext`
- Updated `UserService.cs` and `ToDoService.cs` to use `_db.Users` and `_db.ToDos`
- Applied initial migration with:
```
  dotnet ef migrations add InitialCreate
  dotnet ef database update
```
### 🧪 Example EF Querie
```
_db.Users.ToList();           // Get all users
_db.ToDos.Find(id);           // Find task by ID
_db.SaveChanges();            // Persist changes
```
---

## 📊 Pagination, Filtering & Sorting

Implemented dynamic querying for both `Tasks` and `Users`:

### ✅ Tasks Endpoint (GET /api/tasks) Supports:

- `?search=urgent` → filter by task name
- `?isCompleted=true` → filter by completion status
- `?sortBy=task_desc` → sort by task name descending
- `?page=2&pageSize=5` → paginate results

### ✅ Users Endpoint (GET /api/user) Supports:

- `?search=gmail` → filter by email
- `?sortBy=email_desc` → sort by email descending
- `?page=1&pageSize=10` → paginate results

### 🧠 Technical Highlights

- Used LINQ with EF Core for efficient query building
- Registered services with correct lifetimes (`AddScoped`)
- Updated service interfaces to support new query methods
- Enhanced controller endpoints with query parameters
- Returned structured responses using `ApiResponse<T>`

---

## ⚡ Async/Await Integration for Scalable API Performance

The project has been fully upgraded to use **asynchronous programming** with `async/await` across all service and controller layers. This ensures non-blocking I/O and improved responsiveness under load — a critical best practice for real-world APIs.

### ✅ Highlights

- 🔄 Converted all service methods to `async Task<T>` signatures
- 🧠 Replaced synchronous EF Core calls with:
  - `ToListAsync()` for collections
  - `FindAsync()` for primary key lookups
  - `SaveChangesAsync()` for database commits
- 📡 Updated controller actions to use `async Task<IActionResult>` and `await` service calls
- 🔍 Applied async logic to both **Tasks** and **Users** endpoints

### 🧩 Benefits

- 🚀 Improved scalability and throughput under concurrent requests
- 🧵 Frees up threads in ASP.NET Core thread pool
- 🧪 Easier to test and extend with cancellation tokens or background tasks

### 📁 Affected Files

| File                  | Change Summary                        |
|-----------------------|----------------------------------------|
| `TasksController.cs`  | All actions now use `async/await`      |
| `UsersController.cs`  | Async support for user endpoints       |
| `ToDoService.cs`      | EF Core async methods applied          |
| `UserService.cs`      | Fully async service logic              |
| `IToDoService.cs`     | Interface updated with async signatures |
| `IUserService.cs`     | Interface updated with async signatures |

---

## 📚 Future Enhancements

- 🔐 Add JWT authentication and role-based authorization
- ⚡ Refactor services to use async EF Core methods
- 🧪 Implement unit and integration testing with xUnit
- 📊 Add filtering, sorting, and pagination to endpoints
- 🌱 Seed initial data during migrations or startup
- 📄 Improve Swagger docs with XML comments and examples
- 🧰 Integrate structured logging (e.g., Serilog)
- 🌐 Support API versioning (/api/v1, /api/v2)
- 🧠 Add caching for frequently accessed endpoints
- 🔗 Model relationships (e.g., User → ToDos) with .Include()




