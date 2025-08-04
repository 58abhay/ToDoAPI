

using MediatR;
using ToDoAPI.Application.CQRS.TaskModule.Commands;
using ToDoAPI.Application.Interfaces;
using ToDoAPI.Domain.Entities;
using ToDoAPI.Domain.Exceptions;

namespace ToDoAPI.Application.CQRS.TaskModule.Handlers
{
    public class CreateTaskItemHandler : IRequestHandler<CreateTaskItemCommand, TaskItem>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IAccountRepository _accountRepository;

        public CreateTaskItemHandler(ITaskRepository taskRepository, IAccountRepository accountRepository)
        {
            _taskRepository = taskRepository;
            _accountRepository = accountRepository;
        }

        public async Task<TaskItem> Handle(CreateTaskItemCommand request, CancellationToken cancellationToken)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(request.Description))
                errors.Add("Description cannot be empty.");

            if (request.AccountId == Guid.Empty)
                errors.Add("AccountId must be a valid GUID.");

            if (errors.Any())
                throw new ValidationException(errors);

            var newTask = new TaskItem
            {
                Description = request.Description,
                IsCompleted = request.IsCompleted,
                AccountId = request.AccountId // Corrected property name
            };

            return await _taskRepository.CreateAsync(newTask, cancellationToken);
        }
    }
}