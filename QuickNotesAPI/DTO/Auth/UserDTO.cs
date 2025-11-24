using System.ComponentModel.DataAnnotations;

namespace QuickNotesAPI.DTO.UserDTO
{
    public class UserDTO
    {
        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
