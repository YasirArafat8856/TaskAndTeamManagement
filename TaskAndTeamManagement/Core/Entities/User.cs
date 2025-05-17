using System.ComponentModel.DataAnnotations;


namespace TaskAndTeamManagement.Core.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; } 
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; } 

        public ICollection<Team> AssignedTeams { get; set; } 
        public ICollection<Team> CreatedTeams { get; set; } 
        public ICollection<Task> AssignedTasks { get; set; } 
        public ICollection<Task> CreatedTasks { get; set; } 
        public ICollection<Notification> Notifications { get; set; } 
    }
}
