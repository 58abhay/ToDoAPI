using ToDoAPI.Domain.Entities;
using ToDoAPI.Application.DTOs;

namespace ToDoAPI.Application.Interfaces
{
    public interface ITaskService
    {
        Task<List<TaskItem>> GetAllAsync();
        Task<TaskItem?> GetByIdAsync(int id);
        Task<TaskItem> CreateAsync(CreateTaskDto input);
        Task<TaskItem?> UpdateAsync(int id, UpdateTaskDto input);
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