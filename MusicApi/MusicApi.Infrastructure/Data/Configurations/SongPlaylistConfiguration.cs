using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicApi.MusicApi.Domain.Entities;

namespace MusicApi.MusicApi.Infrastructure.Data.Configurations
{
    public class SongPlaylistConfiguration : IEntityTypeConfiguration<SongPlayList>
    {
        public void Configure(EntityTypeBuilder<SongPlayList> builder)
        {
            builder.HasKey(sp => new { sp.SongId, sp.PlaylistId });

            // one-many with song table
            builder.HasOne(sp => sp.Song)
                .WithMany(s => s.SongPlaylist)
                .HasForeignKey(sp => sp.SongId);

            builder.HasOne(sp => sp.Playlist)
                .WithMany(p => p.SongPlaylist)
                .HasForeignKey(sp => sp.PlaylistId);
        }
    }
}
