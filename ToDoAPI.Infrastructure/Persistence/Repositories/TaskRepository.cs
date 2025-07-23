using Microsoft.EntityFrameworkCore;
using ToDoAPI.Application.Interfaces;
using ToDoAPI.Domain.Entities;
using ToDoAPI.Infrastructure.Persistence;

namespace ToDoAPI.Infrastructure.Persistence.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _db;

        public TaskRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<TaskItem>> GetAllAsync()
        {
            return await _db.TaskItems
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            return await _db.TaskItems.FindAsync(id);
        }

        public async Task<TaskItem> CreateAsync(TaskItem task)
        {
            _db.TaskItems.Add(task);
            await _db.SaveChangesAsync();
            return task;
        }

        public async Task<TaskItem?> UpdateAsync(int id, TaskItem updated)
        {
            var existing = await _db.TaskItems.FindAsync(id);
            if (existing is null) return null;

            existing.Description = updated.Description;
            existing.IsCompleted = updated.IsCompleted;

            await _db.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var task = await _db.TaskItems.FindAsync(id);
            if (task is null) return false;

            _db.TaskItems.Remove(task);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<TaskItem>> GetFilteredAsync(
            string? search,
            string? sortBy,
            bool? isCompleted,
            int page,
            int pageSize)
        {
            var query = _db.TaskItems.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(t => EF.Functions.Like(t.Description, $"%{search}%"));

            if (isCompleted.HasValue)
                query = query.Where(t => t.IsCompleted == isCompleted.Value);

            query = sortBy?.ToLower() switch
            {
                "description" => query.OrderBy(t => t.Description),
                "description_desc" => query.OrderByDescending(t => t.Description),
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