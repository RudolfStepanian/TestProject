using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models.DTO
{
    public class UserRegistrationDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
