using MusicApi.MusicApi.Application.DTOs.PlaylistDTOs;
using MusicApi.MusicApi.Application.DTOs.SongDTOs;

namespace MusicApi.MusicApi.Application.Interfaces
{
    public interface IPlaylistService
    {
        Task<PlaylistResponseDTO> GetPlaylist(Guid playlistId,CancellationToken ct = default);
        Task UpdatePlaylist(Guid playlistId, UpdatePlaylistDTO updatePlaylistDto, CancellationToken ct = default);
        Task<PlaylistResponseDTO> CreatePlaylist(CreatePlaylistDTO createPlaylistDto, CancellationToken ct = default);
        Task DeletePlaylist(Guid playlistId, CancellationToken ct = default);
        Task<PlaylistResponseDTO> AddSongAsync(Guid playlistId, AddSongDTO songDto, CancellationToken ct = default);
        Task RemoveSongAsync(Guid playlistId, Guid songId, CancellationToken ct = default);
        Task<IEnumerable<PlaylistResponseDTO>> GetByUserIdAsync(Guid userId, CancellationToken ct = default);

    }
}
