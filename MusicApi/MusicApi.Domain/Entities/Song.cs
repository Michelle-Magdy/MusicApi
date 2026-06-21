using MusicApi.MusicApi.Domain.Exceptions;

namespace MusicApi.MusicApi.Domain.Entities
{
    public class Song : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Artist { get; set; } = string.Empty;
        private readonly List<SongPlayList> _songPlaylist = new();
        public IReadOnlyCollection<SongPlayList> SongPlaylist => _songPlaylist.AsReadOnly();

        private Song() { }

        public static Song Create(string title, string artist)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new DomainException("title cannot be null");
            }

            if (string.IsNullOrEmpty(artist))
            {
                throw new DomainException("Song should have an artist");
            }

            return new Song
            {
                Id = Guid.NewGuid(),
                Title = title.Trim(),
                Artist = artist.Trim(),
                CreatedAt = DateTime.UtcNow
            };
        }

    }
}
