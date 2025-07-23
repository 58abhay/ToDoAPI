using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoAPI.Application.DTOs;
using ToDoAPI.Application.CQRS.TaskModule.Commands;
using ToDoAPI.Application.CQRS.TaskModule.Queries;
using ToDoAPI.Domain.Entities;
using ToDoAPI.Domain.Wrappers;
using ToDoAPI.Domain.Exceptions;

namespace ToDoAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskItemsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaskItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        private string? TraceId => HttpContext.Items["CorrelationId"]?.ToString();

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<TaskItem>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<IEnumerable<TaskItem>>>> GetAll(
            [FromQuery] string? search,
            [FromQuery] string? sortBy,
            [FromQuery] bool? isCompleted,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var query = new GetTaskItemListQuery(search, sortBy, isCompleted, page, pageSize);
            var items = await _mediator.Send(query);

            return Ok(new ApiResponse<IEnumerable<TaskItem>>(
                items,
                items.Any() ? $"Tasks retrieved [TraceId: {TraceId}]" : $"No tasks found [TraceId: {TraceId}]"
            ));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<TaskItem?>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<TaskItem?>>> GetById(int id)
        {
            var query = new GetTaskItemByIdQuery(id);
            var item = await _mediator.Send(query);

            if (item is null)
                throw new NotFoundException($"Task with ID {id} not found");

            return Ok(new ApiResponse<TaskItem?>(item, $"Task retrieved [TraceId: {TraceId}]"));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<TaskItem>), StatusCodes.Status201Created)]
        public async Task<ActionResult<ApiResponse<TaskItem>>> Create([FromBody] CreateTaskDto input)
        {
            var command = new CreateTaskItemCommand(input.Description, input.IsCompleted);
            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, new ApiResponse<TaskItem>(
                result,
                $"Task created [TraceId: {TraceId}]"
            ));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<TaskItem?>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<TaskItem?>>> Update(int id, [FromBody] UpdateTaskDto input)
        {
            var command = new UpdateTaskItemCommand(id, input.Description, input.IsCompleted);
            var result = await _mediator.Send(command);

            if (result is null)
                throw new NotFoundException($"Task with ID {id} not found");

            return Ok(new ApiResponse<TaskItem?>(result, $"Task updated [TraceId: {TraceId}]"));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse<string?>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<string?>>> Delete(int id)
        {
            var command = new DeleteTaskItemCommand(id);
            var success = await _mediator.Send(command);

            if (!success)
                throw new NotFoundException($"Task with ID {id} not found");

            return Ok(new ApiResponse<string?>($"Task deleted [TraceId: {TraceId}]"));
        }
    }
}