using MediatR;
using ToDoAPI.Application.CQRS.TaskModule.Commands;
using ToDoAPI.Application.Interfaces;
using ToDoAPI.Domain.Entities;

namespace ToDoAPI.Application.CQRS.TaskModule.Handlers
{
    public class UpdateTaskItemHandler : IRequestHandler<UpdateTaskItemCommand, TaskItem?>
    {
        private readonly ITaskRepository _repository;

        public UpdateTaskItemHandler(ITaskRepository repository)
        {
            _repository = repository;
        }

        public async Task<TaskItem?> Handle(UpdateTaskItemCommand request, CancellationToken cancellationToken)
        {
            var existing = await _repository.GetByIdAsync(request.Id);
            if (existing is null) return null;

            existing.Description = request.Description;
            existing.IsCompleted = request.IsCompleted;

            return await _repository.UpdateAsync(request.Id, existing);
        }
    }
}