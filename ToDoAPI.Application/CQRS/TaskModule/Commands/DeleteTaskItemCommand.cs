using MediatR;

namespace ToDoAPI.Application.CQRS.TaskModule.Commands
{
    public record DeleteTaskItemCommand(int Id) : IRequest<bool>;
}