using ToDoApi.Models;
using ToDoApi.Models.DTOs;
using ToDoApi.Services.Interfaces;

namespace ToDoApi.Services
{
    public class ToDoService : IToDoService
    {
        private readonly List<ToDo> _tasks = new();
        private int _nextId = 1;

        public List<ToDo> GetAll() => _tasks;

        public ToDo GetById(int id) => _tasks.FirstOrDefault(t => t.Id == id);

        public ToDo Create(CreateToDoDto input)
        {
            var task = new ToDo { Id = _nextId++, Task = input.Task, IsCompleted = input.IsCompleted };
            _tasks.Add(task);
            return task;
        }

        public ToDo Update(int id, UpdateToDoDto input)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task == null) return null;
            task.Task = input.Task;
            task.IsCompleted = input.IsCompleted;
            return task;
        }

        public bool Delete(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task == null) return false;
            _tasks.Remove(task);
            return true;
        }
    }
}