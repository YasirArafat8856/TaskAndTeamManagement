using System.ComponentModel.DataAnnotations;

namespace TaskAndTeamManagement.DTO.Task
{
    public class AddTaskDto
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; } = "Todo";
        public DateTime DueDate { get; set; }
        public int AssignedToUserId { get; set; }
        public int CreatedByUserId { get; set; }
        public int? TeamId { get; set; }
    }
}
