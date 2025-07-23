using MediatR;
using ToDoAPI.Application.CQRS.TaskModule.Commands;
using ToDoAPI.Application.Interfaces;
using ToDoAPI.Domain.Entities;

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
            var newTask = new TaskItem
            {
                Description = request.Description,
                IsCompleted = request.IsCompleted
            };

            return await _repository.CreateAsync(newTask);
        }
    }
}