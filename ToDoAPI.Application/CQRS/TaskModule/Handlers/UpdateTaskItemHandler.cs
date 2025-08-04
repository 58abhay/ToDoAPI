using MediatR;
using ToDoAPI.Application.CQRS.TaskModule.Commands;
using ToDoAPI.Application.Interfaces;
using ToDoAPI.Domain.Entities;
using ToDoAPI.Domain.Exceptions;

namespace ToDoAPI.Application.CQRS.TaskModule.Handlers
{
    public class UpdateTaskItemHandler : IRequestHandler<UpdateTaskItemCommand, TaskItem>
    {
        private readonly ITaskRepository _repository;

        public UpdateTaskItemHandler(ITaskRepository repository)
        {
            _repository = repository;
        }

        public async Task<TaskItem> Handle(UpdateTaskItemCommand request, CancellationToken cancellationToken)
        {
            var existing = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (existing is null)
                throw new NotFoundException($"Task with ID {request.Id} not found");

            existing.Description = request.Description;
            existing.IsCompleted = request.IsCompleted;

            var updated = await _repository.UpdateAsync(request.Id, existing, cancellationToken);

            return updated!; //  Suppress CS8603 — you already validated it's safe
        }
    }
}