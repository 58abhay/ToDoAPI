using Microsoft.AspNetCore.Mvc;
using ToDoApi.Models;
using ToDoApi.Models.DTOs;
using ToDoApi.Services.Interfaces;

namespace ToDoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _Service;

        public UserController(IUserService service)
        {
            _Service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAll(
                [FromQuery] string? search,
                [FromQuery] string? sortBy,
                [FromQuery] int page = 1,
                [FromQuery] int pageSize = 10)
        {
            var users = _Service.GetFiltered(search, sortBy, page, pageSize);
            if (!users.Any())
            {
                return Ok(new ApiResponse<IEnumerable<User>>(true, "No User Found - list is empty", users));
            }
            return Ok(new ApiResponse<IEnumerable<User>>(true, "Filtered Users fetched successfully", users));
            //var message = users.Any() ? "All Users fetched successfully" : "No User Found - list is empty";
            //return Ok(new ApiResponse<IEnumerable<User>>(true, message, users));
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetById(int id)
        {
            var user = _Service.GetById(id);
            if (user == null)
                return NotFound(new ApiResponse<User>(false, "User not found", null));

            return Ok(new ApiResponse<User>(true, "User found and fetched successfully", user));
        }

        [HttpPost]
        public ActionResult<User> Create([FromBody] CreateUserDto input)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<string>(false, "Invalid input", null));

            var user = _Service.Create(input);
            return CreatedAtAction(nameof(GetById), new { id = user.UserId }, new ApiResponse<User>(true, "New User created successfully", user));
        }

        [HttpPut("{id}")]
        public ActionResult<User> Update(int id, [FromBody] UpdateUserDto input)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<string>(false, "Invalid update input", null));

            var user = _Service.Update(id, input);
            if (user == null)
                return NotFound(new ApiResponse<User>(false, "User not found", null));

            return Ok(new ApiResponse<User>(true, "User updated successfully", user));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _Service.Delete(id);
            if (!result)
                return NotFound(new ApiResponse<string>(false, "User not found", null));

            return Ok(new ApiResponse<string>(true, "User deleted successfully", null));
        }
    }
}