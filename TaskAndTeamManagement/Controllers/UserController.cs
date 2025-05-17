using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskAndTeamManagement.Core.Entities;
using TaskAndTeamManagement.Core.Interface.Service;
using TaskAndTeamManagement.DTO.User;
using TaskAndTeamManagement.Infrascture.Utility;

namespace TaskAndTeamManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService _userService, IMapper _mapper) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<ReturnUserDto>> GetUserById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound("User not found");
            return Ok(_mapper.Map<ReturnUserDto>(user));
        }

        [HttpGet("get-all-users")]
        public async Task<ActionResult<Pagination<ReturnUserDto>>> GetAllUsers()
        {
            var users = await _userService.GetAllListAsync();
            return Ok(_mapper.Map<IReadOnlyList<ReturnUserDto>>(users));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("add-user")]
        public async Task<IActionResult> AddUser([FromBody] AddUserDto dto)
        {
            var user = _mapper.Map<User>(dto);
            await _userService.AddDataAsync(user);
            return Ok("User created successfully");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update-user")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto dto)
        {
            var user = await _userService.GetByIdAsync(dto.Id);
            if (user == null) return NotFound("User not found");
            _mapper.Map(dto, user);
            await _userService.UpdateDataAsync(user);
            return Ok("User updated successfully");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-user/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteAsync(id);
            return Ok("User deleted successfully");
        }
    }
}
