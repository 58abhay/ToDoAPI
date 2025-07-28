

using ToDoAPI.Application.DTOs;
using ToDoAPI.Application.Interfaces;
using ToDoAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ToDoAPI.Application.Configuration;

namespace ToDoAPI.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repo;
        private readonly ILogger<TaskService> _logger;
        private readonly AppSettings _settings;

        public TaskService(
            ITaskRepository repo,
            ILogger<TaskService> logger,
            AppSettings settings)
        {
            _repo = repo;
            _logger = logger;
            _settings = settings;
        }

        public async Task<List<TaskItem>> GetAllAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching all tasks");
            return await _repo.GetAllAsync(cancellationToken);
        }

        public async Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching task by ID: {TaskId}", id);
            return await _repo.GetByIdAsync(id, cancellationToken);
        }

        public async Task<TaskItem> CreateAsync(CreateTaskDto input, CancellationToken cancellationToken)
        {
            var taskCount = await _repo.CountByUserAsync(input.AccountId, cancellationToken);

            if (taskCount >= _settings.MaxTasksPerUser)
            {
                _logger.LogWarning("User {UserId} exceeded max task limit ({Limit})", input.AccountId, _settings.MaxTasksPerUser);
                throw new InvalidOperationException($"Max tasks limit of {_settings.MaxTasksPerUser} reached.");
            }

            var task = new TaskItem
            {
                Description = input.Description,
                IsCompleted = input.IsCompleted,
                AccountId = input.AccountId
            };

            _logger.LogInformation("Creating new task for user {UserId}: {Description}", input.AccountId, input.Description);
            return await _repo.CreateAsync(task, cancellationToken);
        }

        public async Task<TaskItem?> UpdateAsync(Guid id, UpdateTaskDto input, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Updating task {TaskId}", id);
            var existing = await _repo.GetByIdAsync(id, cancellationToken);
            if (existing is null)
            {
                _logger.LogWarning("Task {TaskId} not found for update", id);
                return null;
            }

            existing.Description = input.Description;
            existing.IsCompleted = input.IsCompleted;

            _logger.LogInformation("Task {TaskId} updated successfully", id);
            return await _repo.UpdateAsync(id, existing, cancellationToken);
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Deleting task {TaskId}", id);
            return await _repo.DeleteAsync(id, cancellationToken);
        }

        public async Task<List<TaskItem>> GetFilteredAsync(
            string? search,
            string? sortBy,
            bool? isCompleted,
            int page,
            int pageSize,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Filtering tasks | Search: {Search} | SortBy: {SortBy} | Completed: {IsCompleted} | Page: {Page} | PageSize: {PageSize}",
                search, sortBy, isCompleted, page, pageSize);

            return await _repo.GetFilteredAsync(search, sortBy, isCompleted, page, pageSize, cancellationToken);
        }
    }
}