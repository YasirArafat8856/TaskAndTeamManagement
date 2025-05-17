using System.ComponentModel.DataAnnotations;

namespace TaskAndTeamManagement.DTO.User
{
    public class UpdateUserDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
