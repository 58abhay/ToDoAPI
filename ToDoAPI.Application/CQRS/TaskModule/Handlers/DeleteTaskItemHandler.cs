using MediatR;
using ToDoAPI.Application.CQRS.TaskModule.Commands;
using ToDoAPI.Application.Interfaces;
using ToDoAPI.Domain.Exceptions;

namespace ToDoAPI.Application.CQRS.TaskModule.Handlers
{
    public class DeleteTaskItemHandler : IRequestHandler<DeleteTaskItemCommand, bool>
    {
        private readonly ITaskRepository _repository;

        public DeleteTaskItemHandler(ITaskRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteTaskItemCommand request, CancellationToken cancellationToken)
        {
            var success = await _repository.DeleteAsync(request.Id, cancellationToken);

            if (!success)
                throw new NotFoundException($"Task with ID {request.Id} not found");

            return true;
        }
    }
}