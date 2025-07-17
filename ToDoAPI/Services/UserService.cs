//using Microsoft.EntityFrameworkCore;
//using ToDoApi.Data;
//using ToDoApi.Models;
//using ToDoApi.Models.DTOs;
//using ToDoApi.Services.Interfaces;

//namespace ToDoApi.Services
//{
//    public class UserService : IUserService
//    {
//        private readonly AppDbContext _db;

//        public UserService(AppDbContext db)
//        {
//            _db = db;
//        }

//        public List<User> GetAll()
//        {
//            return _db.Users.ToList(); // Retrieves all users from DB
//        }

//        public User GetById(int id)
//        {
//            return _db.Users.Find(id); // Fetches user by primary key
//        }

//        public User Create(CreateUserDto input)
//        {
//            var user = new User
//            {
//                UserEmail = input.UserEmail,
//                Password = input.Password
//            };
//            _db.Users.Add(user); // Adds to tracking
//            _db.SaveChanges();   // Commits to PostgreSQL
//            return user;
//        }

//        public User Update(int id, UpdateUserDto input)
//        {
//            var user = _db.Users.Find(id);
//            if (user == null) return null;

//            user.UserEmail = input.UserEmail;
//            user.Password = input.Password;
//            _db.SaveChanges();
//            return user;
//        }

//        public bool Delete(int id)
//        {
//            var user = _db.Users.Find(id);
//            if (user == null) return false;

//            _db.Users.Remove(user);
//            _db.SaveChanges();
//            return true;
//        }

//        public List<User> GetFiltered(string? search, string? sortBy, int page, int pageSize)
//        {
//            var query = _db.Users.AsQueryable();

//            // Filtering
//            if (!string.IsNullOrWhiteSpace(search))
//                query = query.Where(u => u.UserEmail.Contains(search));

//            // Sorting
//            query = sortBy?.ToLower() switch
//            {
//                "email" => query.OrderBy(u => u.UserEmail),
//                "email_desc" => query.OrderByDescending(u => u.UserEmail),
//                "id_desc" => query.OrderByDescending(u => u.UserId),
//                _ => query.OrderBy(u => u.UserId)
//            };

//            // Pagination
//            int skip = (page - 1) * pageSize;
//            query = query.Skip(skip).Take(pageSize);

//            return query.ToList();
//        }
//    }
//}


using Microsoft.EntityFrameworkCore;
using ToDoApi.Data;
using ToDoApi.Models;
using ToDoApi.Models.DTOs;
using ToDoApi.Services.Interfaces;

namespace ToDoApi.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _db;

        public UserService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _db.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
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

        public async Task<User> UpdateAsync(int id, UpdateUserDto input)
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
            var query = _db.Users.AsQueryable();

            // Filtering
            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(u => u.UserEmail.Contains(search));

            // Sorting
            query = sortBy?.ToLower() switch
            {
                "email" => query.OrderBy(u => u.UserEmail),
                "email_desc" => query.OrderByDescending(u => u.UserEmail),
                "id_desc" => query.OrderByDescending(u => u.UserId),
                _ => query.OrderBy(u => u.UserId)
            };

            // Pagination
            int skip = (page - 1) * pageSize;
            query = query.Skip(skip).Take(pageSize);

            return await query.ToListAsync();
        }
    }
}