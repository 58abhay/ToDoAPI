

using MediatR;
using System;

namespace ToDoAPI.Application.CQRS.TaskModule.Commands
{
    public record DeleteTaskItemCommand(Guid Id) : IRequest<bool>;
}