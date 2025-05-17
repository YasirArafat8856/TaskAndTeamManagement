using TaskAndTeamManagement.DTO.Team;
using TaskAndTeamManagement.DTO.User;

namespace TaskAndTeamManagement.DTO.Task
{
    public class ReturnTaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime DueDate { get; set; }
        public ReturnUserDto AssignedToUser { get; set; }
        public ReturnUserDto CreatedByUser { get; set; }
        public ReturnTeamDto AssignedTeam { get; set; }
    }
}
