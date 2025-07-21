//namespace ToDoAPI.Domain.Entities
//{
//    public class ToDo
//    {
//        public int Id { get; set; }
//        public string Task { get; set; }
//        public bool IsCompleted { get; set; }
//    }
//}

namespace ToDoAPI.Domain.Entities
{
    public class ToDo
    {
        public int Id { get; set; }

        public required string Task { get; set; }

        public bool IsCompleted { get; set; }
    }
}