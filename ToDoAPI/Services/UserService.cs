//using ToDoApi.Models;
//using ToDoApi.Models.DTOs;
//using ToDoApi.Services.Interfaces;

//namespace ToDoApi.Services
//{
//    public class UserService : IUserService
//    {
//        private readonly List<User> _users = new();
//        private int _nextId = 1;

//        public List<User> GetAll() => _users;

//        public User GetById(int id) => _users.FirstOrDefault(u => u.UserId == id);

//        public User Create(CreateUserDto input)
//        {
//            var user = new User { UserId = _nextId++, UserEmail = input.UserEmail, Password = input.Password };
//            _users.Add(user);
//            return user;
//        }

//        public User Update(int id, UpdateUserDto input)
//        {
//            var user = _users.FirstOrDefault(u => u.UserId == id);
//            if (user == null) return null;
//            user.UserEmail = input.UserEmail;
//            user.Password = input.Password;
//            return user;
//        }

//        public bool Delete(int id)
//        {
//            var user = _users.FirstOrDefault(u => u.UserId == id);
//            if (user == null) return false;
//            _users.Remove(user);
//            return true;
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

        public List<User> GetAll()
        {
            return _db.Users.ToList(); // Retrieves all users from DB
        }

        public User GetById(int id)
        {
            return _db.Users.Find(id); // Fetches user by primary key
        }

        public User Create(CreateUserDto input)
        {
            var user = new User
            {
                UserEmail = input.UserEmail,
                Password = input.Password
            };
            _db.Users.Add(user); // Adds to tracking
            _db.SaveChanges();   // Commits to PostgreSQL
            return user;
        }

        public User Update(int id, UpdateUserDto input)
        {
            var user = _db.Users.Find(id);
            if (user == null) return null;

            user.UserEmail = input.UserEmail;
            user.Password = input.Password;
            _db.SaveChanges();
            return user;
        }

        public bool Delete(int id)
        {
            var user = _db.Users.Find(id);
            if (user == null) return false;

            _db.Users.Remove(user);
            _db.SaveChanges();
            return true;
        }
    }
}