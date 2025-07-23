using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoAPI.Application.DTOs;
using ToDoAPI.Application.CQRS.AccountModule.Commands;
using ToDoAPI.Application.CQRS.AccountModule.Queries;
using ToDoAPI.Domain.Entities;
using ToDoAPI.Domain.Wrappers;

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
                accounts.Any() ? "Accounts retrieved" : "No accounts found"
            ));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<AccountProfile?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<AccountProfile?>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<AccountProfile?>>> GetById(int id)
        {
            var query = new GetAccountProfileByIdQuery(id);
            var account = await _mediator.Send(query);

            if (account is null)
                return NotFound(new ApiResponse<AccountProfile?>("Account not found"));

            return Ok(new ApiResponse<AccountProfile?>(account, "Account retrieved"));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<AccountProfile>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<string?>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<AccountProfile>>> Create([FromBody] CreateAccountDto input)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(new ApiResponse<string?>("Invalid input", errors));
            }

            var command = new CreateAccountProfileCommand(input.Email, input.Password);
            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, new ApiResponse<AccountProfile>(result, "Account created"));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<AccountProfile?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<AccountProfile?>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<string?>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<AccountProfile?>>> Update(int id, [FromBody] UpdateAccountDto input)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(new ApiResponse<string?>("Invalid update input", errors));
            }

            var command = new UpdateAccountProfileCommand(id, input.Email, input.Password);
            var result = await _mediator.Send(command);

            if (result is null)
                return NotFound(new ApiResponse<AccountProfile?>("Account not found"));

            return Ok(new ApiResponse<AccountProfile?>(result, "Account updated"));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse<string?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string?>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<string?>>> Delete(int id)
        {
            var command = new DeleteAccountProfileCommand(id);
            var success = await _mediator.Send(command);

            if (!success)
                return NotFound(new ApiResponse<string?>("Account not found"));

            return Ok(new ApiResponse<string?>("Account deleted"));
        }
    }
}