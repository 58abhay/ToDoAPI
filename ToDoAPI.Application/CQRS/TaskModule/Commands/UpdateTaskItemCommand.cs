using MediatR;
using ToDoAPI.Domain.Entities;

namespace ToDoAPI.Application.CQRS.TaskModule.Commands
{
    public record UpdateTaskItemCommand(int Id, string Description, bool IsCompleted)
        : IRequest<TaskItem?>;
}