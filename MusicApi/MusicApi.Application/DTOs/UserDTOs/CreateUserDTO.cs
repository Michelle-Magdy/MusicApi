using System.ComponentModel.DataAnnotations;

namespace MusicApi.MusicApi.Application.DTOs.UserDTOs
{
    public class CreateUserDTO
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; init; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; init; } = string.Empty;
    }
}
