using Microsoft.EntityFrameworkCore;
using ToDoAPI.Application.Interfaces;
using ToDoAPI.Domain.Entities;
using ToDoAPI.Infrastructure.Persistence;
using System.Threading;

namespace ToDoAPI.Infrastructure.Persistence.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _db;

        public TaskRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<TaskItem>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _db.TaskItems
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<TaskItem?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _db.TaskItems
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
        }

        public async Task<TaskItem> CreateAsync(TaskItem task, CancellationToken cancellationToken)
        {
            await _db.TaskItems.AddAsync(task, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            return task;
        }

        public async Task<TaskItem?> UpdateAsync(int id, TaskItem updated, CancellationToken cancellationToken)
        {
            var existing = await _db.TaskItems.FindAsync(new object[] { id }, cancellationToken);
            if (existing is null) return null;

            existing.Description = updated.Description;
            existing.IsCompleted = updated.IsCompleted;

            await _db.SaveChangesAsync(cancellationToken);
            return existing;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var task = await _db.TaskItems.FindAsync(new object[] { id }, cancellationToken);
            if (task is null) return false;

            _db.TaskItems.Remove(task);
            await _db.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<List<TaskItem>> GetFilteredAsync(
            string? search,
            string? sortBy,
            bool? isCompleted,
            int page,
            int pageSize,
            CancellationToken cancellationToken)
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

            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            Console.WriteLine(query.ToQueryString());
            return await query.ToListAsync(cancellationToken);
        }
    }
}