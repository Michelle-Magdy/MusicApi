using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicApi.MusicApi.Domain.Entities;

namespace MusicApi.MusicApi.Infrastructure.Data.Configurations
{
    public class SongConfiguration : IEntityTypeConfiguration<Song>
    {
        public void Configure(EntityTypeBuilder<Song> builder)
        {
            builder.Property(song => song.Title)
                .HasMaxLength(250)
                .IsRequired();
            builder.Property(song => song.Artist)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(song => song.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
