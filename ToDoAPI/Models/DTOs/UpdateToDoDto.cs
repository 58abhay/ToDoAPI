using System.ComponentModel.DataAnnotations;

namespace ToDoApi.Models.DTOs
{
    public class UpdateToDoDto
    {
        //[Required]
        public string Task { get; set; }

        public bool IsCompleted { get; set; }
    }
}