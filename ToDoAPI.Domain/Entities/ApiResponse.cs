using System.Text.Json.Serialization;

namespace ToDoAPI.Domain.Wrappers
{
    public class ApiResponse<T>
    {
        [JsonPropertyName("success")]
        public bool Success => StatusCode is >= 200 and < 300;

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("data")]
        public T? Data { get; set; }

        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }

        [JsonPropertyName("errors")]
        public List<string> Errors { get; set; } = new();

        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        [JsonPropertyName("traceId")]
        public string? TraceId { get; set; }

        // Success constructor
        public ApiResponse(T data, string message = "Success", int statusCode = 200, string? traceId = null)
        {
            Data = data;
            Message = message;
            StatusCode = statusCode;
            TraceId = traceId;
        }

        // Error constructor
        public ApiResponse(string message, List<string>? errors = null, int statusCode = 400, string? traceId = null)
        {
            Message = message;
            StatusCode = statusCode;
            Errors = errors ?? new List<string>();
            TraceId = traceId;
        }
    }
}