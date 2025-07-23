using System.ComponentModel.DataAnnotations;

namespace ToDoAPI.Application.DTOs
{
    public class UpdateTaskDto
    {
        [Required(ErrorMessage = "Description is required")]
        [MinLength(3)]
        public string Description { get; set; } = string.Empty;

        public bool IsCompleted { get; set; }
    }
}