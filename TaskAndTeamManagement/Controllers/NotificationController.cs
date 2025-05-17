using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskAndTeamManagement.Core.Entities;
using TaskAndTeamManagement.Core.Interface.Service;
using TaskAndTeamManagement.DTO.Notification;

namespace TaskAndTeamManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public NotificationController(INotificationService notificationService, IMapper mapper)
        {
            _notificationService = notificationService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReturnNotificationDto>> GetNotificationById(int id)
        {
            var notification = await _notificationService.GetByIdAsync(id);
            if (notification == null) return NotFound("Notification not found");
            return Ok(_mapper.Map<ReturnNotificationDto>(notification));
        }
        [HttpGet("user-specific-{uid}")]
        public async Task<ActionResult<ReturnNotificationDto>> GetUserSpedificNotificationByUserId(int uid)
        {
            //var notification = await _notificationService.GetByIdAsync(id);
            //if (notification == null) return NotFound("Notification not found");
            //return Ok(_mapper.Map<ReturnNotificationDto>(notification));
            throw new NotImplementedException();
        }

       
    }
}
