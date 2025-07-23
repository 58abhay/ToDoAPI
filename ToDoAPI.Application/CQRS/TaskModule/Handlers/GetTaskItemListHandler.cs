using MediatR;
using ToDoAPI.Application.Interfaces;
using ToDoAPI.Application.CQRS.TaskModule.Queries;
using ToDoAPI.Domain.Entities;
using ToDoAPI.Domain.Exceptions;

namespace ToDoAPI.Application.CQRS.TaskModule.Handlers
{
    public class GetTaskItemListHandler : IRequestHandler<GetTaskItemListQuery, List<TaskItem>>
    {
        private readonly ITaskRepository _repository;

        public GetTaskItemListHandler(ITaskRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<TaskItem>> Handle(GetTaskItemListQuery request, CancellationToken cancellationToken)
        {
            var tasks = await _repository.GetFilteredAsync(
                request.Search,
                request.SortBy,
                request.IsCompleted,
                request.Page,
                request.PageSize,
                cancellationToken
            );

            // Optional: Uncomment if want to force 404 for empty lists
            // if (tasks is null || tasks.Count == 0)
            //     throw new NotFoundException("No task items found");

            return tasks;
        }
    }
}