using MediatR;
using ToDoAPI.Application.Interfaces;
using ToDoAPI.Application.CQRS.TaskModule.Queries;
using ToDoAPI.Domain.Entities;
using ToDoAPI.Domain.Exceptions;

namespace ToDoAPI.Application.CQRS.TaskModule.Handlers
{
    public class GetTaskItemByIdHandler : IRequestHandler<GetTaskItemByIdQuery, TaskItem>
    {
        private readonly ITaskRepository _repository;

        public GetTaskItemByIdHandler(ITaskRepository repository)
        {
            _repository = repository;
        }

        public async Task<TaskItem> Handle(GetTaskItemByIdQuery request, CancellationToken cancellationToken)
        {
            var task = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (task is null)
                throw new NotFoundException($"Task with ID {request.Id} not found");

            return task;
        }
    }
}