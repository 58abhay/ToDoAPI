

namespace ToDoAPI.Application.DTOs
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
    }
}
