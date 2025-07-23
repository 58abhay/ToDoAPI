using ToDoAPI.Domain.Entities;

namespace ToDoAPI.Application.Interfaces
{
    public interface ITaskRepository
    {
        Task<List<TaskItem>> GetAllAsync();
        Task<TaskItem?> GetByIdAsync(int id);
        Task<TaskItem> CreateAsync(TaskItem task);
        Task<TaskItem?> UpdateAsync(int id, TaskItem updated);
        Task<bool> DeleteAsync(int id);
        Task<List<TaskItem>> GetFilteredAsync(
            string? search,
            string? sortBy,
            bool? isCompleted,
            int page,
            int pageSize
        );
    }
}