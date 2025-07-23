using MediatR;

namespace ToDoAPI.Application.CQRS.AccountModule.Commands
{
    public record DeleteAccountProfileCommand(int Id) : IRequest<bool>;
}