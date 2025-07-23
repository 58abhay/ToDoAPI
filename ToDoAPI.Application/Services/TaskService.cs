using ToDoAPI.Application.DTOs;
using ToDoAPI.Application.Interfaces;
using ToDoAPI.Domain.Entities;

namespace ToDoAPI.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repo;

        public TaskService(ITaskRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<TaskItem>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task<TaskItem?> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);

        public async Task<TaskItem> CreateAsync(CreateTaskDto input)
        {
            var task = new TaskItem
            {
                Description = input.Description,
                IsCompleted = input.IsCompleted
            };

            return await _repo.CreateAsync(task);
        }

        public async Task<TaskItem?> UpdateAsync(int id, UpdateTaskDto input)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing is null) return null;

            existing.Description = input.Description;
            existing.IsCompleted = input.IsCompleted;

            return await _repo.UpdateAsync(id, existing);
        }

        public async Task<bool> DeleteAsync(int id) => await _repo.DeleteAsync(id);

        public async Task<List<TaskItem>> GetFilteredAsync(
            string? search,
            string? sortBy,
            bool? isCompleted,
            int page,
            int pageSize)
            => await _repo.GetFilteredAsync(search, sortBy, isCompleted, page, pageSize);
    }
}