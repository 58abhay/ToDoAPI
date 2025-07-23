using System.ComponentModel.DataAnnotations;

namespace ToDoAPI.Application.DTOs
{
    public class CreateTaskDto
    {
        [Required(ErrorMessage = "Description is required")]
        [MinLength(3, ErrorMessage = "Minimum 3 characters")]
        public string Description { get; set; } = string.Empty;

        public bool IsCompleted { get; set; }
    }
}