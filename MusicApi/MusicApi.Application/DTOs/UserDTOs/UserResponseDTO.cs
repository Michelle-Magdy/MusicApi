using System.ComponentModel.DataAnnotations;

namespace MusicApi.MusicApi.Application.DTOs.UserDTOs
{
    public class UserResponseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
    }
}
