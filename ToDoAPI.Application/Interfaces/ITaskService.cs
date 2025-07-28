

using ToDoAPI.Domain.Entities;
using ToDoAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ToDoAPI.Application.Interfaces
{
    public interface ITaskService
    {
        Task<List<TaskItem>> GetAllAsync(CancellationToken cancellationToken);

        Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken); //  Changed from int to Guid

        Task<TaskItem> CreateAsync(CreateTaskDto input, CancellationToken cancellationToken);

        Task<TaskItem?> UpdateAsync(Guid id, UpdateTaskDto input, CancellationToken cancellationToken); //  Changed from int to Guid

        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken); //  Changed from int to Guid

        Task<List<TaskItem>> GetFilteredAsync(
            string? search,
            string? sortBy,
            bool? isCompleted,
            int page,
            int pageSize,
            CancellationToken cancellationToken
        );
    }
}