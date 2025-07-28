

using System.ComponentModel.DataAnnotations;

namespace ToDoAPI.Application.DTOs
{
    public class UpdateTaskDto
    {
        [Required(ErrorMessage = "Description is required")]
        [MinLength(3, ErrorMessage = "Minimum 3 characters")]
        [Display(Name = "Task Description")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Is Task Completed")]
        public bool IsCompleted { get; set; }
    }
}