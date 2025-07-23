using System.Text.Json.Serialization;

namespace ToDoAPI.Domain.Wrappers
{
    public class ApiResponse<T>
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("data")]
        public T? Data { get; set; }

        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }

        [JsonPropertyName("errors")]
        public List<string>? Errors { get; set; }

        // 🟢 Success constructor
        public ApiResponse(T data, string message = "Success", int statusCode = 200)
        {
            Success = true;
            Data = data;
            Message = message;
            StatusCode = statusCode;
            Errors = null;
        }

        // 🔴 Error constructor
        public ApiResponse(string message, List<string>? errors = null, int statusCode = 400)
        {
            Success = false;
            Data = default;
            Message = message;
            StatusCode = statusCode;
            Errors = errors;
        }
    }
}