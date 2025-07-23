using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoAPI.Application.DTOs;
using ToDoAPI.Application.CQRS.TaskModule.Commands;
using ToDoAPI.Application.CQRS.TaskModule.Queries;
using ToDoAPI.Domain.Entities;
using ToDoAPI.Domain.Wrappers;

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

            return Ok(new ApiResponse<IEnumerable<TaskItem>>(items, items.Any() ? "Tasks retrieved" : "No tasks found"));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<TaskItem?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<TaskItem?>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<TaskItem?>>> GetById(int id)
        {
            var query = new GetTaskItemByIdQuery(id);
            var item = await _mediator.Send(query);

            if (item is null)
                return NotFound(new ApiResponse<TaskItem?>("Task not found"));

            return Ok(new ApiResponse<TaskItem?>(item, "Task retrieved"));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<TaskItem>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<string?>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<TaskItem>>> Create([FromBody] CreateTaskDto input)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(new ApiResponse<string?>("Invalid input", errors));
            }

            var command = new CreateTaskItemCommand(input.Description, input.IsCompleted);
            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, new ApiResponse<TaskItem>(result, "Task created"));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<TaskItem?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<TaskItem?>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<string?>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<TaskItem?>>> Update(int id, [FromBody] UpdateTaskDto input)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(new ApiResponse<string?>("Invalid update input", errors));
            }

            var command = new UpdateTaskItemCommand(id, input.Description, input.IsCompleted);
            var result = await _mediator.Send(command);

            if (result is null)
                return NotFound(new ApiResponse<TaskItem?>("Task not found"));

            return Ok(new ApiResponse<TaskItem?>(result, "Task updated"));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse<string?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string?>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<string?>>> Delete(int id)
        {
            var command = new DeleteTaskItemCommand(id);
            var success = await _mediator.Send(command);

            if (!success)
                return NotFound(new ApiResponse<string?>("Task not found"));

            return Ok(new ApiResponse<string?>("Task deleted"));
        }
    }
}