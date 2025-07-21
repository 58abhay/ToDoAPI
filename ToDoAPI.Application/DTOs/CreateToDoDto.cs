//using System.ComponentModel.DataAnnotations;

//namespace ToDoAPI.Application.DTOs
//{
//    public class CreateToDoDto
//    {
//        //[Required]
//        public string Task { get; set; }

//        public bool IsCompleted { get; set; }
//    }
//}

using System.ComponentModel.DataAnnotations;

namespace ToDoAPI.Application.DTOs
{
    public class CreateToDoDto
    {
        [Required]
        public string Task { get; set; } = string.Empty;

        public bool IsCompleted { get; set; }
    }
}