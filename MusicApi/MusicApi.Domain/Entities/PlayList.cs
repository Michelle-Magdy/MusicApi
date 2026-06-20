namespace MusicApi.MusicApi.Domain.Entities
{
    public class PlayList :BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public User User { get; set; } = default!;
        public ICollection<Song> Songs { get; set; } = [];
    }
}
