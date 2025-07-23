using ToDoAPI.Application.DTOs;
using ToDoAPI.Application.Interfaces;
using ToDoAPI.Domain.Entities;

namespace ToDoAPI.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repo;

        public AccountService(IAccountRepository repo)
        {
            _repo = repo;
        }

        public Task<List<AccountProfile>> GetAllAsync() => _repo.GetAllAsync();

        public Task<AccountProfile?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);

        public async Task<AccountProfile> CreateAsync(CreateAccountDto input)
        {
            var account = new AccountProfile
            {
                Email = input.Email,
                Password = input.Password
            };

            return await _repo.CreateAsync(account);
        }

        public async Task<AccountProfile?> UpdateAsync(int id, UpdateAccountDto input)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing is null) return null;

            existing.Email = input.Email;
            existing.Password = input.Password;

            return await _repo.UpdateAsync(id, existing);
        }

        public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);

        public Task<List<AccountProfile>> GetFilteredAsync(string? search, string? sortBy, int page, int pageSize)
            => _repo.GetFilteredAsync(search, sortBy, page, pageSize);
    }
}