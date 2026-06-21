using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicApi.MusicApi.Domain.Entities;

namespace MusicApi.MusicApi.Infrastructure.Data.Configurations
{
    public class PlayListConfiguration : IEntityTypeConfiguration<PlayList>
    {
        public void Configure(EntityTypeBuilder<PlayList> builder)
        {
            builder.Property(playlist => playlist.Name)
               .HasMaxLength(100)
               .IsRequired();

            builder.Property(playlist => playlist.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            // Map the collection to the private backing field
            builder.Metadata.FindNavigation(nameof(PlayList.SongPlaylist))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(p => p.SongPlaylist)
                .WithOne( sp => sp.Playlist)
                .HasForeignKey(sp => sp.PlaylistId);
        }      
    }
}
