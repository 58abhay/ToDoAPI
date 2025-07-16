using System.ComponentModel.DataAnnotations;

namespace ToDoApi.Models.DTOs
{
    public class UpdateUserDto
    {
        [Required]
        [EmailAddress]
        public string UserEmail { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}