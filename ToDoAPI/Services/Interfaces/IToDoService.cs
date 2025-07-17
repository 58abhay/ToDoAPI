//using ToDoApi.Models;
//using ToDoApi.Models.DTOs;

//namespace ToDoApi.Services.Interfaces
//{
//    public interface IToDoService
//    {
//        List<ToDo> GetAll();
//        ToDo GetById(int id);
//        ToDo Create(CreateToDoDto input);
//        ToDo Update(int id, UpdateToDoDto input);
//        bool Delete(int id);
//        List<ToDo> GetFiltered(string? search, string? sortBy, bool? isCompleted, int page, int pageSize);
//    }
//}


using ToDoApi.Models;
using ToDoApi.Models.DTOs;

namespace ToDoApi.Services.Interfaces
{
    public interface IToDoService
    {
        Task<List<ToDo>> GetAllAsync();
        Task<ToDo> GetByIdAsync(int id);
        Task<ToDo> CreateAsync(CreateToDoDto input);
        Task<ToDo> UpdateAsync(int id, UpdateToDoDto input);
        Task<bool> DeleteAsync(int id);
        Task<List<ToDo>> GetFilteredAsync(string? search, string? sortBy, bool? isCompleted, int page, int pageSize);
    }
}