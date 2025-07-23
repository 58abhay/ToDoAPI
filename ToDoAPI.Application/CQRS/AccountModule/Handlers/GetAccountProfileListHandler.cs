using MediatR;
using ToDoAPI.Application.CQRS.AccountModule.Queries;
using ToDoAPI.Application.Interfaces;
using ToDoAPI.Domain.Entities;

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
            return await _repo.GetFilteredAsync(
                request.Search,
                request.SortBy,
                request.Page,
                request.PageSize
            );
        }
    }
}