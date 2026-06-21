using MusicApi.MusicApi.Application.DTOs.PlaylistDTOs;
using MusicApi.MusicApi.Application.DTOs.SongDTOs;
using MusicApi.MusicApi.Application.DTOs.UserDTOs;
using MusicApi.MusicApi.Domain.Entities;
namespace MusicApi.MusicApi.Application.DTOs
{
    public static class MappingDTOs
    {
        public static PlaylistResponseDTO toDto(this PlayList playlist) =>
            new()
            {
                Id = playlist.Id,
                Name = playlist.Name,
                UserId = playlist.UserId,
                Songs = playlist.SongPlaylist.Select(sp => sp.Song.toDto())
            };

        public static SongResponseDTO toDto(this Song song) =>
            new() { 
                Id = song.Id,
                Title = song.Title,
                Artist = song.Artist,
            
            };

        public static UserResponseDTO toDto(this User user) =>
            new() { 
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
            };
    }
}


