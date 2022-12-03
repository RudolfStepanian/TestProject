using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models.DTO
{
    public class UserLoginRequestDto
    {
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }

    }
}
