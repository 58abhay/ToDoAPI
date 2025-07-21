using ToDoAPI.Application.DTOs;
using ToDoAPI.Domain.Entities;

namespace ToDoAPI.Application.Interfaces
{
    public interface IToDoRepository
    {
        Task<List<ToDo>> GetAllAsync();
        Task<ToDo?> GetByIdAsync(int id);
        Task<ToDo> CreateAsync(CreateToDoDto input);
        Task<ToDo?> UpdateAsync(int id, UpdateToDoDto input);
        Task<bool> DeleteAsync(int id);
        Task<List<ToDo>> GetFilteredAsync(string? search, string? sortBy, bool? isCompleted, int page, int pageSize);
    }
}