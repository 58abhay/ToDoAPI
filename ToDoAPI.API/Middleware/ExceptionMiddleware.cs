using System.Net;
using System.Text.Json;
using ToDoAPI.Domain.Exceptions;
using ToDoAPI.Domain.Wrappers;

namespace ToDoAPI.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var traceId = Guid.NewGuid().ToString();
            context.Items["TraceId"] = traceId;

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Unhandled exception [TraceId: {TraceId}]: {Message}", traceId, ex.Message);

                var statusCode = ex switch
                {
                    ValidationException => HttpStatusCode.BadRequest,
                    NotFoundException => HttpStatusCode.NotFound,
                    _ => HttpStatusCode.InternalServerError
                };

                var response = new ApiResponse<string>(
                    message: ex switch
                    {
                        ValidationException => "Validation failed.",
                        NotFoundException nf => nf.Message,
                        _ => "An unexpected error occurred."
                    } + $" [TraceId: {traceId}]",
                    errors: ex switch
                    {
                        ValidationException ve => ve.Errors,
                        NotFoundException => new List<string> { "Resource not found." },
                        _ => new List<string> { ex.Message }
                    },
                    statusCode: (int)statusCode
                );

                context.Response.StatusCode = (int)statusCode;
                context.Response.ContentType = "application/json";

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                };

                var json = JsonSerializer.Serialize(response, options);
                await context.Response.WriteAsync(json);
            }
        }
    }
}