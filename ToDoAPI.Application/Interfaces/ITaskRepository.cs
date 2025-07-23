using ToDoAPI.Domain.Entities;
using System.Threading;

namespace ToDoAPI.Application.Interfaces
{
    public interface ITaskRepository
    {
        Task<List<TaskItem>> GetAllAsync(CancellationToken cancellationToken);
        Task<TaskItem?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<TaskItem> CreateAsync(TaskItem newTask, CancellationToken cancellationToken);
        Task<TaskItem?> UpdateAsync(int id, TaskItem existing, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);

        Task<List<TaskItem>> GetFilteredAsync(
            string? search,
            string? sortBy,
            bool? isCompleted,
            int page,
            int pageSize,
            CancellationToken cancellationToken
        );
    }
}