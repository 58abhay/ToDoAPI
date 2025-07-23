using MediatR;
using ToDoAPI.Application.Interfaces;
using ToDoAPI.Application.CQRS.TaskModule.Queries;
using ToDoAPI.Domain.Entities;

namespace ToDoAPI.Application.CQRS.TaskModule.Handlers
{
    public class GetTaskItemByIdHandler : IRequestHandler<GetTaskItemByIdQuery, TaskItem?>
    {
        private readonly ITaskRepository _repository;

        public GetTaskItemByIdHandler(ITaskRepository repository)
        {
            _repository = repository;
        }

        public async Task<TaskItem?> Handle(GetTaskItemByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(request.Id);
        }
    }
}