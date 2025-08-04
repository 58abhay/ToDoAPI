

namespace ToDoAPI.Domain.Entities
{
    public class TaskItem
    {
        public Guid Id { get; set; } // Changed from int to Guid

        public required string Description { get; set; }

        public bool IsCompleted { get; set; }

        /// <summary>
        /// The owning user's unique identifier
        /// </summary>
        public Guid AccountId { get; set; }
    }
}