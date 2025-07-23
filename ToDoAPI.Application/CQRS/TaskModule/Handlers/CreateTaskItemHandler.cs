using MediatR;
using ToDoAPI.Application.CQRS.TaskModule.Commands;
using ToDoAPI.Application.Interfaces;
using ToDoAPI.Domain.Entities;
using ToDoAPI.Domain.Exceptions;

namespace ToDoAPI.Application.CQRS.TaskModule.Handlers
{
    public class CreateTaskItemHandler : IRequestHandler<CreateTaskItemCommand, TaskItem>
    {
        private readonly ITaskRepository _repository;

        public CreateTaskItemHandler(ITaskRepository repository)
        {
            _repository = repository;
        }

        public async Task<TaskItem> Handle(CreateTaskItemCommand request, CancellationToken cancellationToken)
        {
            // Optional internal validation (FluentValidation handles this too)
            if (string.IsNullOrWhiteSpace(request.Description))
                throw new ValidationException(new List<string> { "Description cannot be empty." });

            var newTask = new TaskItem
            {
                Description = request.Description,
                IsCompleted = request.IsCompleted
            };

            return await _repository.CreateAsync(newTask, cancellationToken);
        }
    }
}