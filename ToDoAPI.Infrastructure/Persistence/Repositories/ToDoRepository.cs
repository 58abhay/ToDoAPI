using Microsoft.EntityFrameworkCore;
using ToDoAPI.Domain.Entities;
using ToDoAPI.Application.DTOs;
using ToDoAPI.Application.Interfaces;
using ToDoAPI.Infrastructure.Persistence;

namespace ToDoAPI.Infrastructure.Persistence.Repositories
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly AppDbContext _db;

        public ToDoRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<ToDo>> GetAllAsync()
        {
            return await _db.ToDos.AsNoTracking().ToListAsync();
        }

        public async Task<ToDo?> GetByIdAsync(int id)
        {
            return await _db.ToDos.FindAsync(id);
        }

        public async Task<ToDo> CreateAsync(CreateToDoDto input)
        {
            var task = new ToDo { Task = input.Task, IsCompleted = input.IsCompleted };
            _db.ToDos.Add(task);
            await _db.SaveChangesAsync();
            return task;
        }

        public async Task<ToDo?> UpdateAsync(int id, UpdateToDoDto input)
        {
            var task = await _db.ToDos.FindAsync(id);
            if (task == null) return null;

            task.Task = input.Task;
            task.IsCompleted = input.IsCompleted;
            await _db.SaveChangesAsync();
            return task;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var task = await _db.ToDos.FindAsync(id);
            if (task == null) return false;

            _db.ToDos.Remove(task);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<ToDo>> GetFilteredAsync(string? search, string? sortBy, bool? isCompleted, int page, int pageSize)
        {
            var query = _db.ToDos.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(t => EF.Functions.Like(t.Task, $"%{search}%"));

            if (isCompleted.HasValue)
                query = query.Where(t => t.IsCompleted == isCompleted.Value);

            query = sortBy?.ToLower() switch
            {
                "task" => query.OrderBy(t => t.Task),
                "task_desc" => query.OrderByDescending(t => t.Task),
                "id_desc" => query.OrderByDescending(t => t.Id),
                _ => query.OrderBy(t => t.Id)
            };

            int skip = (page - 1) * pageSize;
            query = query.Skip(skip).Take(pageSize);

            Console.WriteLine(query.ToQueryString());
            return await query.ToListAsync();
        }
    }
}