namespace MusicApi.MusicApi.Domain.Entities
{
    public class Song : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Artist { get; set; } = string.Empty;
        public ICollection<PlayList> PlayLists { get; set; } = [];

    }
}
