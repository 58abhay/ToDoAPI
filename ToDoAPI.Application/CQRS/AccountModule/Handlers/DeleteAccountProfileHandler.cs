using MediatR;
using ToDoAPI.Application.CQRS.AccountModule.Commands;
using ToDoAPI.Application.Interfaces;
using ToDoAPI.Domain.Exceptions;

namespace ToDoAPI.Application.CQRS.AccountModule.Handlers
{
    public class DeleteAccountProfileHandler : IRequestHandler<DeleteAccountProfileCommand, bool>
    {
        private readonly IAccountRepository _repo;

        public DeleteAccountProfileHandler(IAccountRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeleteAccountProfileCommand request, CancellationToken cancellationToken)
        {
            var success = await _repo.DeleteAsync(request.Id, cancellationToken);

            if (!success)
                throw new NotFoundException($"Account with ID {request.Id} not found");

            return true;
        }
    }
}