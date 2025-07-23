using MediatR;
using ToDoAPI.Application.CQRS.AccountModule.Commands;
using ToDoAPI.Application.Interfaces;
using ToDoAPI.Domain.Entities;
using ToDoAPI.Domain.Exceptions;

namespace ToDoAPI.Application.CQRS.AccountModule.Handlers
{
    public class UpdateAccountProfileHandler : IRequestHandler<UpdateAccountProfileCommand, AccountProfile>
    {
        private readonly IAccountRepository _repo;

        public UpdateAccountProfileHandler(IAccountRepository repo)
        {
            _repo = repo;
        }

        public async Task<AccountProfile> Handle(UpdateAccountProfileCommand request, CancellationToken cancellationToken)
        {
            var updatedAccount = new AccountProfile
            {
                Email = request.Email,
                Password = request.Password
            };

            var result = await _repo.UpdateAsync(request.Id, updatedAccount, cancellationToken);

            if (result is null)
                throw new NotFoundException($"Account with ID {request.Id} not found");

            return result;
        }
    }
}