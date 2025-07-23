using Microsoft.EntityFrameworkCore;
using ToDoAPI.Application.Interfaces;
using ToDoAPI.Domain.Entities;
using ToDoAPI.Infrastructure.Persistence;

namespace ToDoAPI.Infrastructure.Persistence.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _db;

        public AccountRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<AccountProfile>> GetAllAsync()
        {
            return await _db.AccountProfiles
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<AccountProfile?> GetByIdAsync(int id)
        {
            return await _db.AccountProfiles.FindAsync(id);
        }

        public async Task<AccountProfile> CreateAsync(AccountProfile account)
        {
            _db.AccountProfiles.Add(account);
            await _db.SaveChangesAsync();
            return account;
        }

        public async Task<AccountProfile?> UpdateAsync(int id, AccountProfile updated)
        {
            var existing = await _db.AccountProfiles.FindAsync(id);
            if (existing is null) return null;

            existing.Email = updated.Email;
            existing.Password = updated.Password;

            await _db.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var account = await _db.AccountProfiles.FindAsync(id);
            if (account is null) return false;

            _db.AccountProfiles.Remove(account);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<AccountProfile>> GetFilteredAsync(string? search, string? sortBy, int page, int pageSize)
        {
            var query = _db.AccountProfiles.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(a => EF.Functions.Like(a.Email, $"%{search}%"));

            query = sortBy?.ToLower() switch
            {
                "email" => query.OrderBy(a => a.Email),
                "email_desc" => query.OrderByDescending(a => a.Email),
                "id_desc" => query.OrderByDescending(a => a.Id),
                _ => query.OrderBy(a => a.Id)
            };

            int skip = (page - 1) * pageSize;
            query = query.Skip(skip).Take(pageSize);

            Console.WriteLine(query.ToQueryString());
            return await query.ToListAsync();
        }
    }
}