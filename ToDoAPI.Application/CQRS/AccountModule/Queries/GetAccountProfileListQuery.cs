using MediatR;
using ToDoAPI.Domain.Entities;

namespace ToDoAPI.Application.CQRS.AccountModule.Queries
{
    public record GetAccountProfileListQuery(
        string? Search,
        string? SortBy,
        int Page = 1,
        int PageSize = 10
    ) : IRequest<List<AccountProfile>>;
}