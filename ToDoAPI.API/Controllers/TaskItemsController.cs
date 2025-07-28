using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoAPI.Application.CQRS.TaskModule.Commands;
using ToDoAPI.Application.CQRS.TaskModule.Queries;
using ToDoAPI.Application.DTOs;
using ToDoAPI.Domain.Entities;
using ToDoAPI.Domain.Exceptions;
using ToDoAPI.Domain.Wrappers;

namespace ToDoAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskItemsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<TaskItemsController> _logger;

        public TaskItemsController(IMediator mediator, ILogger<TaskItemsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        private string? TraceId => HttpContext.Items["CorrelationId"]?.ToString();

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<TaskItem>>>> GetAll(
            [FromQuery] string? search,
            [FromQuery] string? sortBy,
            [FromQuery] bool? isCompleted,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            _logger.LogInformation("GET all tasks | Search: {Search} | SortBy: {SortBy} | Completed: {IsCompleted} | Page: {Page} | PageSize: {PageSize} | TraceId: {TraceId}",
                search, sortBy, isCompleted, page, pageSize, TraceId);

            var query = new GetTaskItemListQuery(search, sortBy, isCompleted, page, pageSize);
            var items = await _mediator.Send(query);

            return Ok(new ApiResponse<IEnumerable<TaskItem>>(
                items,
                items.Any() ? $"Tasks retrieved [TraceId: {TraceId}]" : $"No tasks found [TraceId: {TraceId}]"
            ));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ApiResponse<TaskItem?>>> GetById([FromRoute] Guid id)
        {
            _logger.LogInformation("GET task by ID: {Id} | TraceId: {TraceId}", id, TraceId);

            var query = new GetTaskItemByIdQuery(id);
            var item = await _mediator.Send(query);

            if (item is null)
            {
                _logger.LogWarning("Task with ID {Id} not found | TraceId: {TraceId}", id, TraceId);
                throw new NotFoundException($"Task with ID {id} not found");
            }

            return Ok(new ApiResponse<TaskItem?>(item, $"Task retrieved [TraceId: {TraceId}]"));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<TaskItem>>> Create([FromBody] CreateTaskDto input)
        {
            _logger.LogInformation("POST create task | Description: {Description} | Completed: {IsCompleted} | AccountId: {AccountId} | TraceId: {TraceId}",
                input.Description, input.IsCompleted, input.AccountId, TraceId);

            var command = new CreateTaskItemCommand(input.Description, input.IsCompleted, input.AccountId);
            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, new ApiResponse<TaskItem>(
                result,
                $"Task created [TraceId: {TraceId}]"
            ));
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ApiResponse<TaskItem?>>> Update([FromRoute] Guid id, [FromBody] UpdateTaskDto input)
        {
            _logger.LogInformation("PUT update task | ID: {Id} | Description: {Description} | Completed: {IsCompleted} | TraceId: {TraceId}",
                id, input.Description, input.IsCompleted, TraceId);

            var command = new UpdateTaskItemCommand(id, input.Description, input.IsCompleted);
            var result = await _mediator.Send(command);

            if (result is null)
            {
                _logger.LogWarning("Task with ID {Id} not found for update | TraceId: {TraceId}", id, TraceId);
                throw new NotFoundException($"Task with ID {id} not found");
            }

            return Ok(new ApiResponse<TaskItem?>(result, $"Task updated [TraceId: {TraceId}]"));
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ApiResponse<string?>>> Delete([FromRoute] Guid id)
        {
            _logger.LogInformation("DELETE task | ID: {Id} | TraceId: {TraceId}", id, TraceId);

            var command = new DeleteTaskItemCommand(id);
            var success = await _mediator.Send(command);

            if (!success)
            {
                _logger.LogWarning("Task with ID {Id} not found for deletion | TraceId: {TraceId}", id, TraceId);
                throw new NotFoundException($"Task with ID {id} not found");
            }

            return Ok(new ApiResponse<string?>($"Task deleted [TraceId: {TraceId}]"));
        }
    }
}