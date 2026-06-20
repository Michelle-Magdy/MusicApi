namespace MusicApi.MusicApi.Domain.Entities
{
    public class Song : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Artist { get; set; } = string.Empty;
        private readonly List<SongPlayList> _songPlaylists = new();
        public IReadOnlyCollection<SongPlayList> SongPlayList => _songPlaylists.AsReadOnly();

    }
}
