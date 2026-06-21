using System.ComponentModel.DataAnnotations;

namespace MusicApi.MusicApi.Application.DTOs.PlaylistDTOs
{
    public record AddSongDTO
    {
        [Required]
       public Guid SongId { get; set; }
    }
}
