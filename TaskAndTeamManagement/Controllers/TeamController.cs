using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskAndTeamManagement.Core.Entities;
using TaskAndTeamManagement.Core.Interface.Service;
using TaskAndTeamManagement.DTO.Team;

namespace TaskAndTeamManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;
        private readonly IMapper _mapper;

        public TeamController(ITeamService teamService, IMapper mapper)
        {
            _teamService = teamService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReturnTeamDto>> GetTeamById(int id)
        {
            var team = await _teamService.GetByIdAsync(id);
            if (team == null) return NotFound("Team not found");
            return Ok(_mapper.Map<ReturnTeamDto>(team));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("add-team")]
        public async Task<IActionResult> AddTeam([FromBody] AddTeamDto dto)
        {
            var team = _mapper.Map<Team>(dto);
            await _teamService.AddDataAsync(team);
            return Ok("Team created successfully");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update-team")]
        public async Task<IActionResult> UpdateTeam([FromBody] UpdateTeamDto dto)
        {
            var team = await _teamService.GetByIdAsync(dto.Id);
            if (team == null) return NotFound("Team not found");
            _mapper.Map(dto, team);
            await _teamService.UpdateDataAsync(team);
            return Ok("Team updated successfully");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-team/{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            await _teamService.DeleteAsync(id);
            return Ok("Team deleted successfully");
        }
    }
}
