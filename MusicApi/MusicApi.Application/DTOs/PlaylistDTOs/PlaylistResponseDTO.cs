using MusicApi.MusicApi.Application.DTOs.SongDTOs;

namespace MusicApi.MusicApi.Application.DTOs.PlaylistDTOs;

public class PlaylistResponseDTO
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
   
    public Guid UserId { get; init; }

    public IEnumerable<SongResponseDTO> Songs { get; init; } = [];

}
