using MediatR;
using ToDoAPI.Domain.Entities;

namespace ToDoAPI.Application.CQRS.AccountModule.Commands
{
    public record CreateAccountProfileCommand(string Email, string Password)
        : IRequest<AccountProfile>;
}