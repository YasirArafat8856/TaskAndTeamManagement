using System.ComponentModel.DataAnnotations;

namespace TaskAndTeamManagement.Core.Entities
{
    public class Team
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } 
        public string Description { get; set; }
        public string Status { get; set; } 
        public DateTime DueDate { get; set; }
        public int AssignedToUserId { get; set; }
        public User AssignedToUser { get; set; }
        public int CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; }

        public int AssignedTeamId { get; set; }
        public Team AssignedTeam { get; set; }
    }
}
