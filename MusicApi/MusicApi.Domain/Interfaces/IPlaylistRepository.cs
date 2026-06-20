using MusicApi.MusicApi.Domain.Entities;

namespace MusicApi.MusicApi.Domain.Interfaces
{
    public interface IPlaylistRepository
    {
        Task<PlayList?> GetByIdAsync(Guid id,CancellationToken ct = default);
        Task<PlayList?> GetByIdWithSongsAsync(Guid id, CancellationToken ct = default);
        Task<IEnumerable<PlayList>> GetByUserIdAsync(Guid userId, CancellationToken ct = default);
        Task AddAsync(PlayList playlist, CancellationToken ct = default);
        Task UpdateAsync(PlayList playlist, CancellationToken ct = default);

        Task DeleteAsync(Guid id, CancellationToken ct = default);

        Task SaveChangesAsync(CancellationToken ct = default);
    }
}
