using System.ComponentModel.DataAnnotations;

namespace MusicApi.MusicApi.Application.DTOs.PlaylistDTOs
{
    public record CreatePlaylistDTO
    {
        [Required(ErrorMessage = "Playlist name is required")]
        [MaxLength(100,ErrorMessage ="Max length is 100 charachters")]
        public string Name { get; set; } = string.Empty;

        [Required]
        public Guid userId { get; set; }
    }
}
