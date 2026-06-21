using MusicApi.MusicApi.Application.DTOs.SongDTOs;

namespace MusicApi.MusicApi.Application.Interfaces
{
    public interface ISongService
    {
        Task<SongResponseDTO?> GetSong(Guid id, CancellationToken ct = default);
        Task<SongResponseDTO> CreateSong(CreateSongDTO createSongDTO, CancellationToken ct = default);
        Task UpdateSong(Guid id,UpdateSongDTO updateSongDTO, CancellationToken ct = default);
        Task DeleteSong(Guid id, CancellationToken ct = default);
    }
}
