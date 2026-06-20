namespace MusicApi.MusicApi.Domain.Entities
{
    public class SongPlayList
    {
        public Guid PlaylistId { get; set; }
        public required PlayList Playlist { get; set; }

        public Guid SongId { get; set;}
        public required Song Song { get; set; }

        public DateTime AddedAt { get; set; } = DateTime.UtcNow;

    }
}
