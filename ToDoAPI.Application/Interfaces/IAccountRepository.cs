using ToDoAPI.Domain.Entities;

namespace ToDoAPI.Application.Interfaces
{
    public interface IAccountRepository
    {
        Task<List<AccountProfile>> GetAllAsync();
        Task<AccountProfile?> GetByIdAsync(int id);
        Task<AccountProfile> CreateAsync(AccountProfile account);
        Task<AccountProfile?> UpdateAsync(int id, AccountProfile updated);
        Task<bool> DeleteAsync(int id);
        Task<List<AccountProfile>> GetFilteredAsync(
            string? search,
            string? sortBy,
            int page,
            int pageSize
        );
    }
}