

using MediatR;
using ToDoAPI.Domain.Entities;

namespace ToDoAPI.Application.CQRS.TaskModule.Queries
{
    public record GetTaskItemByIdQuery(Guid Id) : IRequest<TaskItem>;
}