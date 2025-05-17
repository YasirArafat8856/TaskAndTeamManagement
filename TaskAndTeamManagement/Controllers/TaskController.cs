using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskAndTeamManagement.Core.Interface.Service;
using TaskAndTeamManagement.Core.Specifications.TaskSpec;
using TaskAndTeamManagement.DTO.Task;
using TaskAndTeamManagement.DTO.User;
using TaskAndTeamManagement.Infrascture.Implementation.Service;
using TaskAndTeamManagement.Infrascture.Utility;

namespace TaskAndTeamManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController(ITaskService _taskService, IMapper _mapper) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<ReturnTaskDto>> GetTaskById(int id)
        {
            var task = await _taskService.GetByIdAsync(id);
            if (task == null) return NotFound("Task not found");
            return Ok(_mapper.Map<ReturnTaskDto>(task));
        }

        [HttpGet("get-all-task-list")]
        public async Task<ActionResult<Pagination<ReturnTaskDto>>> GetAllUsers([FromQuery] TaskSpecParams specParams)
        {
            specParams.Search ??= string.Empty;
            var spec = new TaskSpecification(specParams);
            var countSpec = new TaskFilterCountSpecification(specParams);

            var totalItems = await _taskService.CountAsync(countSpec);
            var entityList = await _taskService.GetAllListAsync(spec);

            var data = _mapper.Map<IReadOnlyList<Core.Entities.Task>, IReadOnlyList<ReturnTaskDto>>(entityList);

            return Ok(new Pagination<ReturnTaskDto>(specParams.PageIndex, specParams.PageSize, totalItems, data));
        }

        [Authorize(Roles = "Manager")]
        [HttpPost("add-task")]
        public async Task<IActionResult> AddTask([FromBody] AddTaskDto dto)
        {
            var task = _mapper.Map<Core.Entities.Task>(dto);
            await _taskService.AddDataAsync(task);
            return Ok("Task created successfully");
        }

        [Authorize(Roles = "Manager,Employee")]
        [HttpPut("update-task")]
        public async Task<IActionResult> UpdateTask([FromBody] UpdateTaskDto dto)
        {
            var task = await _taskService.GetByIdAsync(dto.Id);
            if (task == null) return NotFound("Task not found");
            _mapper.Map(dto, task);
            await _taskService.UpdateDataAsync(task);
            return Ok("Task updated successfully");
        }

        [Authorize(Roles = "Manager")]
        [HttpDelete("delete-task/{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            await _taskService.DeleteAsync(id);
            return Ok("Task deleted successfully");
        }
    }
}
