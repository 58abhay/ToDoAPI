using ToDoApi.Models;
using ToDoApi.Models.DTOs;

namespace ToDoApi.Services.Interfaces
{
    public interface IUserService
    {
        List<User> GetAll();
        User GetById(int id);
        User Create(CreateUserDto input);
        User Update(int id, UpdateUserDto input);
        bool Delete(int id);
        List<User> GetFiltered(string? search, string? sortBy, int page, int pageSize);
    }
}