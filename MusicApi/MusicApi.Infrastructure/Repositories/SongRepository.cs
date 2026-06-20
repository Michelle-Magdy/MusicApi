using Microsoft.EntityFrameworkCore;
using MusicApi.MusicApi.Domain.Entities;
using MusicApi.MusicApi.Domain.Interfaces;
using MusicApi.MusicApi.Infrastructure.Data;

namespace MusicApi.MusicApi.Infrastructure.Repositories
{

    public class SongRepository : ISongRepository
    {

        private readonly AppDbContext _context;
        public SongRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Song song, CancellationToken ct = default)
        {
            await _context.Songs.AddAsync(song);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct = default)
        {
            await _context.Songs.Where(s => s.Id == id).ExecuteDeleteAsync(ct);
        }

        public async Task<IEnumerable<Song?>> GetAllAsync(CancellationToken ct = default)
        {
            return await _context.Songs.ToListAsync(ct);
        }

        public async Task<Song?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _context.Songs.FirstOrDefaultAsync(s => s.Id == id,ct);
        }

        public async Task SaveChangesAsync(CancellationToken ct = default)
        {
            await _context.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(Song song, CancellationToken ct = default)
        {
            await _context.Songs.Where(s => s.Id == song.Id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(s => s.Title,song.Title)
                    .SetProperty(s => s.Artist,song.Artist)
                    , ct);
        }
    }
}
