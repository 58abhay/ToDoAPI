//namespace ToDoAPI.Domain.Entities
//{
//    public class User
//    {
//        public int UserId { get; set; }
//        public string UserEmail { get; set; }
//        public string Password { get; set; }
//    }
//}


namespace ToDoAPI.Domain.Entities
{
    public class AccountProfile
    {
        public int Id { get; set; }

        public required string Email { get; set; }

        public required string Password { get; set; }
    }
}