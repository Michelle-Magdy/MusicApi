namespace MusicApi.MusicApi.Domain.Entities
{
    public class SongPlayList
    {
        public Guid PlaylistId { get; set; }
        public PlayList? Playlist { get; set; } = null;

        public Guid SongId { get; set;}
        public Song? Song { get; set; } = null;

        public DateTime AddedAt { get; set; } = DateTime.UtcNow;

    }
}
