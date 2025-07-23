using ToDoAPI.Domain.Entities;
using ToDoAPI.Application.DTOs;
using System.Threading;

namespace ToDoAPI.Application.Interfaces
{
    public interface ITaskService
    {
        Task<List<TaskItem>> GetAllAsync(CancellationToken cancellationToken);
        Task<TaskItem?> GetByIdAsync(int id, CancellationToken cancellationToken); // Made nullable
        Task<TaskItem> CreateAsync(CreateTaskDto input, CancellationToken cancellationToken);
        Task<TaskItem?> UpdateAsync(int id, UpdateTaskDto input, CancellationToken cancellationToken);
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