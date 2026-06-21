namespace MusicApi.MusicApi.Application.DTOs.SongDTOs
{
    public class SongResponseDTO
    {
        public Guid Id { get; init; }
        public string Title { get; init; } = string.Empty;
        public string Artist { get; init; } = string.Empty;
    }
}
