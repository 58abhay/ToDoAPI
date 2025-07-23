using MediatR;
using ToDoAPI.Application.CQRS.AccountModule.Commands;
using ToDoAPI.Application.Interfaces;
using ToDoAPI.Domain.Entities;

namespace ToDoAPI.Application.CQRS.AccountModule.Handlers
{
    public class UpdateAccountProfileHandler : IRequestHandler<UpdateAccountProfileCommand, AccountProfile?>
    {
        private readonly IAccountRepository _repo;

        public UpdateAccountProfileHandler(IAccountRepository repo)
        {
            _repo = repo;
        }

        public async Task<AccountProfile?> Handle(UpdateAccountProfileCommand request, CancellationToken cancellationToken)
        {
            var account = new AccountProfile
            {
                Email = request.Email,
                Password = request.Password
            };

            return await _repo.UpdateAsync(request.Id, account);
        }
    }
}