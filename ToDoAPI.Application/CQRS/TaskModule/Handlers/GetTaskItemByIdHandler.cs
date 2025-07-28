

using MediatR;
using ToDoAPI.Application.Interfaces;
using ToDoAPI.Application.CQRS.TaskModule.Queries;
using ToDoAPI.Domain.Entities;
using ToDoAPI.Domain.Exceptions;

namespace ToDoAPI.Application.CQRS.TaskModule.Handlers
{
    public class GetTaskItemByIdHandler : IRequestHandler<GetTaskItemByIdQuery, TaskItem>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IAccountRepository _accountRepository;

        public GetTaskItemByIdHandler(ITaskRepository taskRepository, IAccountRepository accountRepository)
        {
            _taskRepository = taskRepository;
            _accountRepository = accountRepository;
        }

        public async Task<TaskItem> Handle(GetTaskItemByIdQuery request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetByIdAsync(request.Id, cancellationToken);

            if (task is null)
                throw new NotFoundException($"Task with ID {request.Id} not found");

            // Future use: account verification or access control via _accountRepository

            return task;
        }
    }
}