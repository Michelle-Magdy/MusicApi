namespace MusicApi.MusicApi.Application.DTOs.PlaylistDTOs
{
    public class PlaylistResponseDTO
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string? Description { get; init; }
        public Guid UserId { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime UpdatedAt { get; init; }
        public IEnumerable<SongResponseDto> Songs { get; init; } = [];
        public int SongCount => Songs.Count();
    }
}
