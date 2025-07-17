//using ToDoApi.Models;
//using ToDoApi.Models.DTOs;

//namespace ToDoApi.Services.Interfaces
//{
//    public interface IUserService
//    {
//        List<User> GetAll();
//        User GetById(int id);
//        User Create(CreateUserDto input);
//        User Update(int id, UpdateUserDto input);
//        bool Delete(int id);
//        List<User> GetFiltered(string? search, string? sortBy, int page, int pageSize);
//    }
//}


using ToDoApi.Models;
using ToDoApi.Models.DTOs;

namespace ToDoApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task<User> CreateAsync(CreateUserDto input);
        Task<User> UpdateAsync(int id, UpdateUserDto input);
        Task<bool> DeleteAsync(int id);
        Task<List<User>> GetFilteredAsync(string? search, string? sortBy, int page, int pageSize);
    }
}