//using System.ComponentModel.DataAnnotations;

//namespace ToDoAPI.Application.DTOs
//{
//    public class UpdateUserDto
//    {
//        //[Required]
//        [EmailAddress]
//        public string UserEmail { get; set; }

//        //[Required]
//        [MinLength(6)]
//        public string Password { get; set; }
//    }
//}

using System.ComponentModel.DataAnnotations;

namespace ToDoAPI.Application.DTOs
{
    public class UpdateUserDto
    {
        [Required]
        [EmailAddress]
        public string UserEmail { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;
    }
}