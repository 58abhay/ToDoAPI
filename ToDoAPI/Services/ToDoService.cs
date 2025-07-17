//using ToDoApi.Models;
//using ToDoApi.Models.DTOs;
//using ToDoApi.Services.Interfaces;

//namespace ToDoApi.Services
//{
//    public class ToDoService : IToDoService
//    {
//        private readonly List<ToDo> _tasks = new();
//        private int _nextId = 1;

//        public List<ToDo> GetAll() => _tasks;

//        public ToDo GetById(int id) => _tasks.FirstOrDefault(t => t.Id == id);

//        public ToDo Create(CreateToDoDto input)
//        {
//            var task = new ToDo { Id = _nextId++, Task = input.Task, IsCompleted = input.IsCompleted };
//            _tasks.Add(task);
//            return task;
//        }

//        public ToDo Update(int id, UpdateToDoDto input)
//        {
//            var task = _tasks.FirstOrDefault(t => t.Id == id);
//            if (task == null) return null;
//            task.Task = input.Task;
//            task.IsCompleted = input.IsCompleted;
//            return task;
//        }

//        public bool Delete(int id)
//        {
//            var task = _tasks.FirstOrDefault(t => t.Id == id);
//            if (task == null) return false;
//            _tasks.Remove(task);
//            return true;
//        }
//    }
//}

using Microsoft.EntityFrameworkCore;
using ToDoApi.Data;
using ToDoApi.Models;
using ToDoApi.Models.DTOs;
using ToDoApi.Services.Interfaces;

namespace ToDoApi.Services
{
    public class ToDoService : IToDoService
    {
        private readonly AppDbContext _db;

        public ToDoService(AppDbContext db)
        {
            _db = db;
        }

        public List<ToDo> GetAll()
        {
            return _db.ToDos.ToList(); // Gets all tasks
        }

        public ToDo GetById(int id)
        {
            return _db.ToDos.Find(id); // Finds task by ID
        }

        public ToDo Create(CreateToDoDto input)
        {
            var task = new ToDo
            {
                Task = input.Task,
                IsCompleted = input.IsCompleted
            };
            _db.ToDos.Add(task);
            _db.SaveChanges();
            return task;
        }

        public ToDo Update(int id, UpdateToDoDto input)
        {
            var task = _db.ToDos.Find(id);
            if (task == null) return null;

            task.Task = input.Task;
            task.IsCompleted = input.IsCompleted;
            _db.SaveChanges();
            return task;
        }

        public bool Delete(int id)
        {
            var task = _db.ToDos.Find(id);
            if (task == null) return false;

            _db.ToDos.Remove(task);
            _db.SaveChanges();
            return true;
        }

        public List<ToDo> GetFiltered(string? search, string? sortBy, bool? isCompleted, int page, int pageSize)
        {
            var query = _db.ToDos.AsQueryable();

            // Filtering
            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(t => t.Task.Contains(search));

            if (isCompleted.HasValue)
                query = query.Where(t => t.IsCompleted == isCompleted.Value);

            // Sorting
            query = sortBy?.ToLower() switch
            {
                "task" => query.OrderBy(t => t.Task),
                "task_desc" => query.OrderByDescending(t => t.Task),
                "id_desc" => query.OrderByDescending(t => t.Id),
                _ => query.OrderBy(t => t.Id)
            };

            // Pagination
            int skip = (page - 1) * pageSize;
            query = query.Skip(skip).Take(pageSize);

            return query.ToList();
        }
    }
}