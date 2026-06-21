using MusicApi.MusicApi.Domain.Exceptions;

namespace MusicApi.MusicApi.Domain.Entities
{
    public class PlayList : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public User User { get; set; } = default!;
        private readonly List<SongPlayList> _songPlaylist = new();

        public IReadOnlyCollection<SongPlayList> SongPlaylist => _songPlaylist.AsReadOnly();

        public static PlayList Create(string name, Guid userId)
        {
            if (string.IsNullOrEmpty(name)) throw new DomainException("playlist must have a name");
            return new()
            {
                Name = name,
                UserId = userId
            };
        }
        public void AddSong(Song song)
        {
            if (song == null) throw new ArgumentNullException(nameof(song));
            if (_songPlaylist.Any(s => s.SongId == song.Id))
            {
                throw new AlreadyExistsException("this song is already in the playlist");
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
            var songPlaylist = _songPlaylist.FirstOrDefault(sp => sp.SongId == songId && sp.PlaylistId == this.Id)
                ?? throw new NotFoundException("Song", songId);

            _songPlaylist.Remove(songPlaylist);
            UpdatedAt = DateTime.UtcNow;
        }


        public void Update(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Playlist name cannot be empty.");

            Name = name.Trim();
            UpdatedAt = DateTime.UtcNow;
        }
    }
 }
