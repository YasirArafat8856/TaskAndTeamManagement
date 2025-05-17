using System.ComponentModel.DataAnnotations;

namespace TaskAndTeamManagement.DTO.User
{
    public class AddUserDto
    {
        [Required]
        public string FullName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
