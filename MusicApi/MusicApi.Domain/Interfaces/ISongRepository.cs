using MusicApi.MusicApi.Domain.Entities;

namespace MusicApi.MusicApi.Domain.Interfaces
{
    public interface ISongRepository
    {
        Task<Song?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<IEnumerable<Song?>> GetAllAsync(CancellationToken ct = default);
        Task AddAsync(Song song, CancellationToken ct = default);
        Task UpdateAsync(Song song, CancellationToken ct = default);

        Task DeleteAsync(Guid id, CancellationToken ct = default);
        Task SaveChangesAsync(CancellationToken ct = default);

    }
}
