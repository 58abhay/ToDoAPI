using ToDoAPI.Domain.Entities;
using System.Threading;

namespace ToDoAPI.Application.Interfaces
{
    public interface IAccountRepository
    {
        Task<List<AccountProfile>> GetAllAsync(CancellationToken cancellationToken);
        Task<AccountProfile?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<AccountProfile> CreateAsync(AccountProfile account, CancellationToken cancellationToken);
        Task<AccountProfile?> UpdateAsync(int id, AccountProfile updatedAccount, CancellationToken cancellationToken);
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