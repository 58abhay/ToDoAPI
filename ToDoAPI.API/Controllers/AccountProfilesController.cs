using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoAPI.Application.DTOs;
using ToDoAPI.Application.CQRS.AccountModule.Commands;
using ToDoAPI.Application.CQRS.AccountModule.Queries;
using ToDoAPI.Domain.Entities;
using ToDoAPI.Domain.Wrappers;
using ToDoAPI.Domain.Exceptions;

namespace ToDoAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountProfilesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountProfilesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        private string? TraceId => HttpContext.Items["CorrelationId"]?.ToString();

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<AccountProfile>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<IEnumerable<AccountProfile>>>> GetAll(
            [FromQuery] string? search,
            [FromQuery] string? sortBy,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var query = new GetAccountProfileListQuery(search, sortBy, page, pageSize);
            var accounts = await _mediator.Send(query);

            return Ok(new ApiResponse<IEnumerable<AccountProfile>>(
                accounts,
                accounts.Any() ? $"Accounts retrieved [TraceId: {TraceId}]" : $"No accounts found [TraceId: {TraceId}]"
            ));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<AccountProfile?>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<AccountProfile?>>> GetById(int id)
        {
            var query = new GetAccountProfileByIdQuery(id);
            var account = await _mediator.Send(query);

            if (account is null)
                throw new NotFoundException($"Account with ID {id} not found");

            return Ok(new ApiResponse<AccountProfile?>(account, $"Account retrieved [TraceId: {TraceId}]"));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<AccountProfile>), StatusCodes.Status201Created)]
        public async Task<ActionResult<ApiResponse<AccountProfile>>> Create([FromBody] CreateAccountDto input)
        {
            var command = new CreateAccountProfileCommand(input.Email, input.Password);
            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, new ApiResponse<AccountProfile>(
                result,
                $"Account created [TraceId: {TraceId}]"
            ));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<AccountProfile?>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<AccountProfile?>>> Update(int id, [FromBody] UpdateAccountDto input)
        {
            var command = new UpdateAccountProfileCommand(id, input.Email, input.Password);
            var result = await _mediator.Send(command);

            if (result is null)
                throw new NotFoundException($"Account with ID {id} not found");

            return Ok(new ApiResponse<AccountProfile?>(result, $"Account updated [TraceId: {TraceId}]"));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse<string?>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<string?>>> Delete(int id)
        {
            var command = new DeleteAccountProfileCommand(id);
            var success = await _mediator.Send(command);

            if (!success)
                throw new NotFoundException($"Account with ID {id} not found");

            return Ok(new ApiResponse<string?>($"Account deleted [TraceId: {TraceId}]"));
        }
    }
}