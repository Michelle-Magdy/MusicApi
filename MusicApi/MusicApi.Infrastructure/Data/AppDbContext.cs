using Microsoft.EntityFrameworkCore;
using MusicApi.MusicApi.Domain.Entities;

namespace MusicApi.MusicApi.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Song> Songs => Set<Song>();
        public DbSet<PlayList> PlayLists => Set<PlayList>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // register entities it finds any class implementing IEntityTypeConfiguration<T>
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

    }
}
