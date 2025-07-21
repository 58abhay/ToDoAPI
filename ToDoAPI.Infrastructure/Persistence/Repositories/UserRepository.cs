using Microsoft.EntityFrameworkCore;
using ToDoAPI.Domain.Entities;
using ToDoAPI.Application.DTOs;
using ToDoAPI.Application.Interfaces;
using ToDoAPI.Infrastructure.Persistence;

namespace ToDoAPI.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;

        public UserRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _db.Users
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _db.Users.FindAsync(id);
        }

        public async Task<User> CreateAsync(CreateUserDto input)
        {
            var user = new User
            {
                UserEmail = input.UserEmail,
                Password = input.Password
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task<User?> UpdateAsync(int id, UpdateUserDto input)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null) return null;

            user.UserEmail = input.UserEmail;
            user.Password = input.Password;
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null) return false;

            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<User>> GetFilteredAsync(string? search, string? sortBy, int page, int pageSize)
        {
            var query = _db.Users
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(u => EF.Functions.Like(u.UserEmail, $"%{search}%"));

            query = sortBy?.ToLower() switch
            {
                "email" => query.OrderBy(u => u.UserEmail),
                "email_desc" => query.OrderByDescending(u => u.UserEmail),
                "id_desc" => query.OrderByDescending(u => u.UserId),
                _ => query.OrderBy(u => u.UserId)
            };

            int skip = (page - 1) * pageSize;
            query = query.Skip(skip).Take(pageSize);

            Console.WriteLine(query.ToQueryString());
            return await query.ToListAsync();
        }
    }
}