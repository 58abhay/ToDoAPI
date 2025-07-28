


namespace ToDoAPI.Domain.Entities
{
    public class AccountProfile
    {
        public int Id { get; set; }

        public required string Email { get; set; }

        public required string Password { get; set; }
    }
}