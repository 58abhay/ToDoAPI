using ToDoAPI.Application.DTOs;
using ToDoAPI.Domain.Entities;

namespace ToDoAPI.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<User> CreateAsync(CreateUserDto input);
        Task<User?> UpdateAsync(int id, UpdateUserDto input);
        Task<bool> DeleteAsync(int id);
        Task<List<User>> GetFilteredAsync(string? search, string? sortBy, int page, int pageSize);
    }
}