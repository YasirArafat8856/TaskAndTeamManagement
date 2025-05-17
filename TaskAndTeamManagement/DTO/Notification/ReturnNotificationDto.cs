using TaskAndTeamManagement.DTO.User;

namespace TaskAndTeamManagement.DTO.Notification
{
    public class ReturnNotificationDto
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public ReturnUserDto User { get; set; }
    }
}
