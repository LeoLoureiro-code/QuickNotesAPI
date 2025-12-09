using System.ComponentModel.DataAnnotations;

namespace QuickNotesAPI.DTO.Auth
{
    public class LoginDTO
    {
        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
        
        [Required]
        public string? RefreshToken { get; set; }
    }
}
