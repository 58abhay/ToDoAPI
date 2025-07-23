using MediatR;
using ToDoAPI.Application.CQRS.AccountModule.Commands;
using ToDoAPI.Application.Interfaces;

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
            return await _repo.DeleteAsync(request.Id);
        }
    }
}