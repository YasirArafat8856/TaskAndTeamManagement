using System.ComponentModel.DataAnnotations;

namespace TaskAndTeamManagement.DTO.Task
{
    public class UpdateTaskDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime DueDate { get; set; }
        public int AssignedToUserId { get; set; }
        public int? TeamId { get; set; }
    }
}
