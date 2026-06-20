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
        }

        
    }
}
