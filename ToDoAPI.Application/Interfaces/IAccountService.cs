using ToDoAPI.Domain.Entities;
using ToDoAPI.Application.DTOs;
using System.Threading;

namespace ToDoAPI.Application.Interfaces
{
    public interface IAccountService
    {
        Task<List<AccountProfile>> GetAllAsync(CancellationToken cancellationToken);
        Task<AccountProfile?> GetByIdAsync(int id, CancellationToken cancellationToken); // Now nullable
        Task<AccountProfile> CreateAsync(CreateAccountDto input, CancellationToken cancellationToken);
        Task<AccountProfile?> UpdateAsync(int id, UpdateAccountDto input, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
        Task<List<AccountProfile>> GetFilteredAsync(
            string? search,
            string? sortBy,
            int page,
            int pageSize,
            CancellationToken cancellationToken
        );
    }
}