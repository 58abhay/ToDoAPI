

using ToDoAPI.Domain.Entities;
using System;
using System.Threading;

namespace ToDoAPI.Application.Interfaces
{
    public interface ITaskRepository
    {
        Task<List<TaskItem>> GetAllAsync(CancellationToken cancellationToken);

        Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken); // Changed from int to Guid

        Task<TaskItem> CreateAsync(TaskItem newTask, CancellationToken cancellationToken);

        Task<TaskItem?> UpdateAsync(Guid id, TaskItem existing, CancellationToken cancellationToken); // Changed from int to Guid

        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken); // Changed from int to Guid

        Task<List<TaskItem>> GetFilteredAsync(
            string? search,
            string? sortBy,
            bool? isCompleted,
            int page,
            int pageSize,
            CancellationToken cancellationToken
        );

        Task<int> CountByUserAsync(Guid userId, CancellationToken cancellationToken);
    }
}