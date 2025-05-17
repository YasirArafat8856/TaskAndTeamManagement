using System.ComponentModel.DataAnnotations;

namespace TaskAndTeamManagement.DTO.Team
{
    public class UpdateTeamDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
