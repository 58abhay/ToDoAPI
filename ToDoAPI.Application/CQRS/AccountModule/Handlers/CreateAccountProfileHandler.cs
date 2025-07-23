using MediatR;
using ToDoAPI.Application.Interfaces;
using ToDoAPI.Application.CQRS.AccountModule.Commands;
using ToDoAPI.Domain.Entities;

namespace ToDoAPI.Application.CQRS.AccountModule.Handlers
{
    public class CreateAccountProfileHandler : IRequestHandler<CreateAccountProfileCommand, AccountProfile>
    {
        private readonly IAccountRepository _repo;

        public CreateAccountProfileHandler(IAccountRepository repo)
        {
            _repo = repo;
        }

        public async Task<AccountProfile> Handle(CreateAccountProfileCommand request, CancellationToken cancellationToken)
        {
            // Optional: add guard clause if your repo doesn't validate
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
                throw new ArgumentException("Email and Password are required.");

            var account = new AccountProfile
            {
                Email = request.Email,
                Password = request.Password
            };

            return await _repo.CreateAsync(account, cancellationToken);
        }
    }
}