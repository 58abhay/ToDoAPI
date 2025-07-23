using MediatR;
using ToDoAPI.Application.Interfaces;
using ToDoAPI.Application.CQRS.AccountModule.Queries;
using ToDoAPI.Domain.Entities;

namespace ToDoAPI.Application.CQRS.AccountModule.Handlers
{
    public class GetAccountProfileByIdHandler : IRequestHandler<GetAccountProfileByIdQuery, AccountProfile?>
    {
        private readonly IAccountRepository _repo;

        public GetAccountProfileByIdHandler(IAccountRepository repo)
        {
            _repo = repo;
        }

        public async Task<AccountProfile?> Handle(GetAccountProfileByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetByIdAsync(request.Id);
        }
    }
}