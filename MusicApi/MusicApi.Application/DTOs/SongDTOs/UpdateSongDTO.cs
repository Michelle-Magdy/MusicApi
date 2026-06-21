using System.ComponentModel.DataAnnotations;

namespace MusicApi.MusicApi.Application.DTOs.SongDTOs
{
    public class UpdateSongDTO
    {
        [Required]
        public Guid id { get; set; }
        [MaxLength(200)]
        public string Title { get; init; } = string.Empty;

        [MaxLength(100)]
        public string Artist { get; init; } = string.Empty;

    }
}
