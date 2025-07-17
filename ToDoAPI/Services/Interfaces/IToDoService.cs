using ToDoApi.Models;
using ToDoApi.Models.DTOs;

namespace ToDoApi.Services.Interfaces
{
    public interface IToDoService
    {
        List<ToDo> GetAll();
        ToDo GetById(int id);
        ToDo Create(CreateToDoDto input);
        ToDo Update(int id, UpdateToDoDto input);
        bool Delete(int id);
        List<ToDo> GetFiltered(string? search, string? sortBy, bool? isCompleted, int page, int pageSize);
    }
}