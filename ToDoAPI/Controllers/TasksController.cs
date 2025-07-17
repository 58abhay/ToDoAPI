//using Microsoft.AspNetCore.Mvc;
//using System.Threading.Tasks;
//using ToDoApi.Models;
//using ToDoApi.Models.DTOs;
//using ToDoApi.Services.Interfaces;

//namespace ToDoApi.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class TasksController : ControllerBase
//    {
//        private readonly IToDoService _Service;

//        public TasksController(IToDoService service)
//        {
//            _Service = service;
//        }

//        [HttpGet]
//        public ActionResult<IEnumerable<ToDo>> GetAll(
//            [FromQuery] string? search,
//            [FromQuery] string? sortBy,
//            [FromQuery] bool? isCompleted,
//            [FromQuery] int page = 1,
//            [FromQuery] int pageSize = 10)
//        {
//            var tasks = _Service.GetFiltered(search, sortBy, isCompleted, page, pageSize);
//            if (!tasks.Any())
//            {
//                return Ok(new ApiResponse<IEnumerable<ToDo>>(true, "No Task Found - list is empty", tasks));
//            }
//            return Ok(new ApiResponse<IEnumerable<ToDo>>(true, "Filtered Tasks fetched successfully", tasks));
//            //var message = tasks.Any() ? "All Tasks fetched successfully" : "No Task Found - list is empty";
//            //return Ok(new ApiResponse<IEnumerable<ToDo>>(true, message, tasks));
//        }

//        [HttpGet("{id}")]
//        public ActionResult<ToDo> GetById(int id)
//        {
//            var task = _Service.GetById(id);
//            if (task == null)
//                return NotFound(new ApiResponse<ToDo>(false, "Task not found", null));

//            return Ok(new ApiResponse<ToDo>(true, "Task found and fetched successfully", task));
//        }

//        [HttpPost]
//        public ActionResult<ToDo> Create([FromBody] CreateToDoDto input)
//        {
//            if (!ModelState.IsValid)
//                return BadRequest(new ApiResponse<string>(false, "Invalid task input", null));

//            var task = _Service.Create(input);
//            return CreatedAtAction(nameof(GetById), new { id = task.Id }, new ApiResponse<ToDo>(true, "New Task created successfully", task));
//        }

//        [HttpPut("{id}")]
//        public ActionResult<ToDo> Update(int id, [FromBody] UpdateToDoDto input)
//        {
//            if (!ModelState.IsValid)
//                return BadRequest(new ApiResponse<string>(false, "Invalid update input", null));

//            var task = _Service.Update(id, input);
//            if (task == null)
//                return NotFound(new ApiResponse<ToDo>(false, "Task not found", null));

//            return Ok(new ApiResponse<ToDo>(true, "Task updated successfully", task));
//        }

//        [HttpDelete("{id}")]
//        public IActionResult Delete(int id)
//        {
//            var result = _Service.Delete(id);
//            if (!result)
//                return NotFound(new ApiResponse<string>(false, "Task not found", null));

//            return Ok(new ApiResponse<string>(true, "Task deleted successfully", null));
//        }
//    }
//}

using Microsoft.AspNetCore.Mvc;
using ToDoApi.Models;
using ToDoApi.Models.DTOs;
using ToDoApi.Services.Interfaces;

namespace ToDoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly IToDoService _Service;

        public TasksController(IToDoService service)
        {
            _Service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDo>>> GetAll(
            [FromQuery] string? search,
            [FromQuery] string? sortBy,
            [FromQuery] bool? isCompleted,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var tasks = await _Service.GetFilteredAsync(search, sortBy, isCompleted, page, pageSize);
            if (!tasks.Any())
            {
                return Ok(new ApiResponse<IEnumerable<ToDo>>(true, "No Task Found - list is empty", tasks));
            }
            return Ok(new ApiResponse<IEnumerable<ToDo>>(true, "Filtered Tasks fetched successfully", tasks));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ToDo>> GetById(int id)
        {
            var task = await _Service.GetByIdAsync(id);
            if (task == null)
                return NotFound(new ApiResponse<ToDo>(false, "Task not found", null));

            return Ok(new ApiResponse<ToDo>(true, "Task found and fetched successfully", task));
        }

        [HttpPost]
        public async Task<ActionResult<ToDo>> Create([FromBody] CreateToDoDto input)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<string>(false, "Invalid task input", null));

            var task = await _Service.CreateAsync(input);
            return CreatedAtAction(nameof(GetById), new { id = task.Id }, new ApiResponse<ToDo>(true, "New Task created successfully", task));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ToDo>> Update(int id, [FromBody] UpdateToDoDto input)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<string>(false, "Invalid update input", null));

            var task = await _Service.UpdateAsync(id, input);
            if (task == null)
                return NotFound(new ApiResponse<ToDo>(false, "Task not found", null));

            return Ok(new ApiResponse<ToDo>(true, "Task updated successfully", task));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _Service.DeleteAsync(id);
            if (!result)
                return NotFound(new ApiResponse<string>(false, "Task not found", null));

            return Ok(new ApiResponse<string>(true, "Task deleted successfully", null));
        }
    }
}