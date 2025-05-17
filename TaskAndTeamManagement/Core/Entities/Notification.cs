using System.ComponentModel.DataAnnotations;

namespace TaskAndTeamManagement.Core.Entities
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; } 
        public DateTime CreatedAt { get; set; } 
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
