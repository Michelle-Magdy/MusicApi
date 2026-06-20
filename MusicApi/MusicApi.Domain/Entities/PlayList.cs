using MusicApi.MusicApi.Domain.Exceptions;

namespace MusicApi.MusicApi.Domain.Entities
{
    public class PlayList :BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public User User { get; set; } = default!;
        private readonly List<SongPlayList> _songPlaylist = new();

        public IReadOnlyCollection<SongPlayList> SongPlaylist => _songPlaylist.AsReadOnly();


        public void AddSong(Song song)
        {
            if(song == null) throw new ArgumentNullException(nameof(song));
            if(_songPlaylist.Any(s => s.SongId == song.Id))
            {
                throw new DomainException("this song is already in the playlist");
            }

            _songPlaylist.Add(new SongPlayList
            {
                SongId = song.Id,
                Song = song,
                PlaylistId = this.Id,
                Playlist = this
            });

            UpdatedAt = DateTime.UtcNow;

        }

        public void RemoveSong(Guid songId)
        {
            var songPlaylist = _songPlaylist.FirstOrDefault(s => s.SongId == songId)
                ?? throw new NotFoundException("Song",songId);

            _songPlaylist.Remove(songPlaylist);
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
