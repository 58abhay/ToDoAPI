using MediatR;
using ToDoAPI.Domain.Entities;

namespace ToDoAPI.Application.CQRS.AccountModule.Commands
{
    public record UpdateAccountProfileCommand(int Id, string Email, string Password)
        : IRequest<AccountProfile>; // Non-nullable, since handler throws if not found
}