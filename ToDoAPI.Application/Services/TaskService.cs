using ToDoAPI.Application.DTOs;
using ToDoAPI.Application.Interfaces;
using ToDoAPI.Domain.Entities;
using System.Threading;

namespace ToDoAPI.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repo;

        public TaskService(ITaskRepository repo)
        {
            _repo = repo;
        }

        public Task<List<TaskItem>> GetAllAsync(CancellationToken cancellationToken)
            => _repo.GetAllAsync(cancellationToken);

        public Task<TaskItem?> GetByIdAsync(int id, CancellationToken cancellationToken)
            => _repo.GetByIdAsync(id, cancellationToken); // ✅ Return type updated

        public async Task<TaskItem> CreateAsync(CreateTaskDto input, CancellationToken cancellationToken)
        {
            var task = new TaskItem
            {
                Description = input.Description,
                IsCompleted = input.IsCompleted
            };

            return await _repo.CreateAsync(task, cancellationToken);
        }

        public async Task<TaskItem?> UpdateAsync(int id, UpdateTaskDto input, CancellationToken cancellationToken)
        {
            var existing = await _repo.GetByIdAsync(id, cancellationToken);
            if (existing is null) return null;

            existing.Description = input.Description;
            existing.IsCompleted = input.IsCompleted;

            return await _repo.UpdateAsync(id, existing, cancellationToken);
        }

        public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
            => _repo.DeleteAsync(id, cancellationToken);

        public Task<List<TaskItem>> GetFilteredAsync(
            string? search,
            string? sortBy,
            bool? isCompleted,
            int page,
            int pageSize,
            CancellationToken cancellationToken)
            => _repo.GetFilteredAsync(search, sortBy, isCompleted, page, pageSize, cancellationToken);
    }
}