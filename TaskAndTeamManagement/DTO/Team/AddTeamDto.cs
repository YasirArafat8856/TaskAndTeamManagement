using System.ComponentModel.DataAnnotations;

namespace TaskAndTeamManagement.DTO.Team
{
    public class AddTeamDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
