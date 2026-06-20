using System.ComponentModel.DataAnnotations;

namespace MusicApi.MusicApi.Application.DTOs.SongDTOs
{
    public class CreateSongDTO
    {
        [Required(ErrorMessage = "Song title is required.")]
        [MaxLength(200)]
        public string Title { get; init; } = string.Empty;

        [Required(ErrorMessage = "Artist name is required.")]
        [MaxLength(100)]
        public string Artist { get; init; } = string.Empty;
    }
}
