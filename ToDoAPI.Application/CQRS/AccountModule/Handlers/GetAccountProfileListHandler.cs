using MediatR;
using ToDoAPI.Application.CQRS.AccountModule.Queries;
using ToDoAPI.Application.Interfaces;
using ToDoAPI.Domain.Entities;
using ToDoAPI.Domain.Exceptions;

namespace ToDoAPI.Application.CQRS.AccountModule.Handlers
{
    public class GetAccountProfileListHandler : IRequestHandler<GetAccountProfileListQuery, List<AccountProfile>>
    {
        private readonly IAccountRepository _repo;

        public GetAccountProfileListHandler(IAccountRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<AccountProfile>> Handle(GetAccountProfileListQuery request, CancellationToken cancellationToken)
        {
            var accounts = await _repo.GetFilteredAsync(
                request.Search,
                request.SortBy,
                request.Page,
                request.PageSize,
                cancellationToken
            );

            // Optional guard if ever want to enforce 404 for empty lists:
            // if (accounts == null || accounts.Count == 0)
            //     throw new NotFoundException("No account profiles found");

            return accounts;
        }
    }
}