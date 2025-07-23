using MediatR;
using ToDoAPI.Domain.Entities;

namespace ToDoAPI.Application.CQRS.TaskModule.Queries
{
    public record GetTaskItemListQuery(
        string? Search,
        string? SortBy,
        bool? IsCompleted,
        int Page,
        int PageSize
    ) : IRequest<List<TaskItem>>;
}