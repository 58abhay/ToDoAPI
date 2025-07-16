using ToDoApi.Models;
using ToDoApi.Models.DTOs;
using ToDoApi.Services.Interfaces;

namespace ToDoApi.Services
{
    public class UserService : IUserService
    {
        private readonly List<User> _users = new();
        private int _nextId = 1;

        public List<User> GetAll() => _users;

        public User GetById(int id) => _users.FirstOrDefault(u => u.UserId == id);

        public User Create(CreateUserDto input)
        {
            var user = new User { UserId = _nextId++, UserEmail = input.UserEmail, Password = input.Password };
            _users.Add(user);
            return user;
        }

        public User Update(int id, UpdateUserDto input)
        {
            var user = _users.FirstOrDefault(u => u.UserId == id);
            if (user == null) return null;
            user.UserEmail = input.UserEmail;
            user.Password = input.Password;
            return user;
        }

        public bool Delete(int id)
        {
            var user = _users.FirstOrDefault(u => u.UserId == id);
            if (user == null) return false;
            _users.Remove(user);
            return true;
        }
    }
}