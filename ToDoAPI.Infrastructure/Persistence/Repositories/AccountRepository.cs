using Microsoft.EntityFrameworkCore;
using ToDoAPI.Application.Interfaces;
using ToDoAPI.Domain.Entities;
using ToDoAPI.Infrastructure.Persistence;
using System.Threading;

namespace ToDoAPI.Infrastructure.Persistence.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _db;

        public AccountRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<AccountProfile>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _db.AccountProfiles
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<AccountProfile?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _db.AccountProfiles
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        }

        public async Task<AccountProfile> CreateAsync(AccountProfile account, CancellationToken cancellationToken)
        {
            await _db.AccountProfiles.AddAsync(account, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            return account;
        }

        public async Task<AccountProfile?> UpdateAsync(int id, AccountProfile updatedAccount, CancellationToken cancellationToken)
        {
            var existing = await _db.AccountProfiles.FindAsync(new object[] { id }, cancellationToken);
            if (existing is null) return null;

            existing.Email = updatedAccount.Email;
            existing.Password = updatedAccount.Password;

            await _db.SaveChangesAsync(cancellationToken);
            return existing;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var account = await _db.AccountProfiles.FindAsync(new object[] { id }, cancellationToken);
            if (account is null) return false;

            _db.AccountProfiles.Remove(account);
            await _db.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<List<AccountProfile>> GetFilteredAsync(
            string? search,
            string? sortBy,
            int page,
            int pageSize,
            CancellationToken cancellationToken)
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

            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            Console.WriteLine(query.ToQueryString());
            return await query.ToListAsync(cancellationToken);
        }
    }
}