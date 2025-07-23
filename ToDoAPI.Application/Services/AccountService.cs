using ToDoAPI.Application.DTOs;
using ToDoAPI.Application.Interfaces;
using ToDoAPI.Domain.Entities;
using System.Threading;

namespace ToDoAPI.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repo;

        public AccountService(IAccountRepository repo)
        {
            _repo = repo;
        }

        public Task<List<AccountProfile>> GetAllAsync(CancellationToken cancellationToken)
            => _repo.GetAllAsync(cancellationToken);

        public Task<AccountProfile?> GetByIdAsync(int id, CancellationToken cancellationToken) // ✅ Match interface return
            => _repo.GetByIdAsync(id, cancellationToken);

        public async Task<AccountProfile> CreateAsync(CreateAccountDto input, CancellationToken cancellationToken)
        {
            var account = new AccountProfile
            {
                Email = input.Email,
                Password = input.Password
            };

            return await _repo.CreateAsync(account, cancellationToken);
        }

        public async Task<AccountProfile?> UpdateAsync(int id, UpdateAccountDto input, CancellationToken cancellationToken)
        {
            var existing = await _repo.GetByIdAsync(id, cancellationToken);
            if (existing is null) return null;

            existing.Email = input.Email;
            existing.Password = input.Password;

            return await _repo.UpdateAsync(id, existing, cancellationToken);
        }

        public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
            => _repo.DeleteAsync(id, cancellationToken);

        public Task<List<AccountProfile>> GetFilteredAsync(
            string? search,
            string? sortBy,
            int page,
            int pageSize,
            CancellationToken cancellationToken)
            => _repo.GetFilteredAsync(search, sortBy, page, pageSize, cancellationToken);
    }
}