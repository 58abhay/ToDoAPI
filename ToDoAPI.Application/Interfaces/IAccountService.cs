using ToDoAPI.Domain.Entities;
using ToDoAPI.Application.DTOs;

namespace ToDoAPI.Application.Interfaces
{
    public interface IAccountService
    {
        Task<List<AccountProfile>> GetAllAsync();
        Task<AccountProfile?> GetByIdAsync(int id);
        Task<AccountProfile> CreateAsync(CreateAccountDto input);
        Task<AccountProfile?> UpdateAsync(int id, UpdateAccountDto input);
        Task<bool> DeleteAsync(int id);
        Task<List<AccountProfile>> GetFilteredAsync(
            string? search,
            string? sortBy,
            int page,
            int pageSize
        );
    }
}