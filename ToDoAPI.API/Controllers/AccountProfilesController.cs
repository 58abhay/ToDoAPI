using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoAPI.Application.CQRS.AccountModule.Commands;
using ToDoAPI.Application.CQRS.AccountModule.Queries;
using ToDoAPI.Application.DTOs;
using ToDoAPI.Domain.Entities;
using ToDoAPI.Domain.Exceptions;
using ToDoAPI.Domain.Wrappers;


namespace ToDoAPI.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AccountProfilesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AccountProfilesController> _logger;

        public AccountProfilesController(IMediator mediator, ILogger<AccountProfilesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        private string? TraceId => HttpContext.Items["CorrelationId"]?.ToString();

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<AccountProfile>>>> GetAll(
            [FromQuery] string? search,
            [FromQuery] string? sortBy,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            _logger.LogInformation("GET all accounts | Search: {Search} | SortBy: {SortBy} | Page: {Page} | PageSize: {PageSize} | TraceId: {TraceId}",
                search, sortBy, page, pageSize, TraceId);

            var query = new GetAccountProfileListQuery(search, sortBy, page, pageSize);
            var accounts = await _mediator.Send(query);

            return Ok(new ApiResponse<IEnumerable<AccountProfile>>(
                accounts,
                accounts.Any() ? $"Accounts retrieved [TraceId: {TraceId}]" : $"No accounts found [TraceId: {TraceId}]"
            ));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<AccountProfile?>>> GetById(int id)
        {
            _logger.LogInformation("GET account by ID: {Id} | TraceId: {TraceId}", id, TraceId);

            var query = new GetAccountProfileByIdQuery(id);
            var account = await _mediator.Send(query);

            if (account is null)
            {
                _logger.LogWarning("Account with ID {Id} not found | TraceId: {TraceId}", id, TraceId);
                throw new NotFoundException($"Account with ID {id} not found");
            }

            return Ok(new ApiResponse<AccountProfile?>(account, $"Account retrieved [TraceId: {TraceId}]"));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<AccountProfile>>> Create([FromBody] CreateAccountDto input)
        {
            _logger.LogInformation("POST create account | Email: {Email} | TraceId: {TraceId}", input.Email, TraceId);

            var command = new CreateAccountProfileCommand(input.Email, input.Password);
            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, new ApiResponse<AccountProfile>(
                result,
                $"Account created [TraceId: {TraceId}]"
            ));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<AccountProfile?>>> Update(int id, [FromBody] UpdateAccountDto input)
        {
            _logger.LogInformation("PUT update account | ID: {Id} | Email: {Email} | TraceId: {TraceId}", id, input.Email, TraceId);

            var command = new UpdateAccountProfileCommand(id, input.Email, input.Password);
            var result = await _mediator.Send(command);

            if (result is null)
            {
                _logger.LogWarning("Account with ID {Id} not found for update | TraceId: {TraceId}", id, TraceId);
                throw new NotFoundException($"Account with ID {id} not found");
            }

            return Ok(new ApiResponse<AccountProfile?>(result, $"Account updated [TraceId: {TraceId}]"));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string?>>> Delete(int id)
        {
            _logger.LogInformation("DELETE account | ID: {Id} | TraceId: {TraceId}", id, TraceId);

            var command = new DeleteAccountProfileCommand(id);
            var success = await _mediator.Send(command);

            if (!success)
            {
                _logger.LogWarning("Account with ID {Id} not found for deletion | TraceId: {TraceId}", id, TraceId);
                throw new NotFoundException($"Account with ID {id} not found");
            }

            return Ok(new ApiResponse<string?>($"Account deleted [TraceId: {TraceId}]"));
        }
    }
}