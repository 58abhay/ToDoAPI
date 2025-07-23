using MediatR;
using ToDoAPI.Domain.Entities;

namespace ToDoAPI.Application.CQRS.TaskModule.Commands
{
    public record CreateTaskItemCommand(string Description, bool IsCompleted)
        : IRequest<TaskItem>;
}