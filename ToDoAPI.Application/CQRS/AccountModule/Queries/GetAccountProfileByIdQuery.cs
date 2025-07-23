using MediatR;
using ToDoAPI.Domain.Entities;

namespace ToDoAPI.Application.CQRS.AccountModule.Queries
{
    public record GetAccountProfileByIdQuery(int Id) : IRequest<AccountProfile>;
}