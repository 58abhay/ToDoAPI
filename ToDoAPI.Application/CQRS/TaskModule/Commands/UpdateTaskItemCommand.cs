

using MediatR;
using ToDoAPI.Domain.Entities;
using System;

namespace ToDoAPI.Application.CQRS.TaskModule.Commands
{
    public record UpdateTaskItemCommand(Guid Id, string Description, bool IsCompleted)
        : IRequest<TaskItem>;
}