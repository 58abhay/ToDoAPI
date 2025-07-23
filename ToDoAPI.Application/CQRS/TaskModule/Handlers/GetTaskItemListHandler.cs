using MediatR;
using ToDoAPI.Application.Interfaces;
using ToDoAPI.Application.CQRS.TaskModule.Queries;
using ToDoAPI.Domain.Entities;

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
            return await _repository.GetFilteredAsync(
                request.Search,
                request.SortBy,
                request.IsCompleted,
                request.Page,
                request.PageSize
            );
        }
    }
}