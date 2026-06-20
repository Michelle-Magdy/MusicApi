using Microsoft.EntityFrameworkCore;
using MusicApi.MusicApi.Domain.Entities;
using MusicApi.MusicApi.Domain.Interfaces;
using MusicApi.MusicApi.Infrastructure.Data;

namespace MusicApi.MusicApi.Infrastructure.Repositories
{
    public class PlaylistRepository : IPlaylistRepository
    {
        private readonly AppDbContext _dbContext; 
        
        public PlaylistRepository(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }

        public async Task AddAsync(PlayList playlist, CancellationToken ct = default)
        {
             await _dbContext.PlayLists.AddAsync(playlist, ct);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct = default)
        {
             await _dbContext.PlayLists.Where(p => p.Id == id).ExecuteDeleteAsync(ct);
        }

        public async Task<PlayList?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _dbContext.PlayLists.FirstOrDefaultAsync( p => p.Id == id,ct);
        }

        public async Task<PlayList?> GetByIdWithSongsAsync(Guid id, CancellationToken ct = default)
        {
            return await _dbContext.PlayLists.Include(p => p.SongPlaylist)
                .ThenInclude(sp => sp.Song)
                .FirstOrDefaultAsync(p => p.Id == id, ct);
        }

        public async Task<IEnumerable<PlayList>> GetByUserIdAsync(Guid userId, CancellationToken ct = default)
        {
            return await _dbContext.PlayLists
                .Include(p => p.SongPlaylist)
                .ThenInclude(sp => sp.Song)
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.UpdatedAt)
                .ToListAsync(ct);
        }

        public async Task SaveChangesAsync(CancellationToken ct = default)
        {
            await _dbContext.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(PlayList playlist, CancellationToken ct = default)
        {
            await _dbContext.PlayLists
                .Where(p => p.Id == playlist.Id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(p => p.Name, playlist.Name)
                    .SetProperty(p => p.UpdatedAt, DateTime.UtcNow)
                ,ct);
        }
    }
}
