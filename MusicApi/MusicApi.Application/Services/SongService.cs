using MusicApi.MusicApi.Application.DTOs;
using MusicApi.MusicApi.Application.DTOs.SongDTOs;
using MusicApi.MusicApi.Application.Interfaces;
using MusicApi.MusicApi.Domain.Entities;
using MusicApi.MusicApi.Domain.Exceptions;
using MusicApi.MusicApi.Domain.Interfaces;

namespace MusicApi.MusicApi.Application.Services
{
    public class SongService : ISongService
    {
        private readonly ISongRepository _songRepo;
        public SongService(ISongRepository songRepo)
        {
            _songRepo = songRepo;
        }

        public async Task<SongResponseDTO> CreateSong(CreateSongDTO createSongDTO,CancellationToken ct =default)
        {
            var song = Song.Create(createSongDTO.Title,createSongDTO.Artist);
            await _songRepo.AddAsync(song,ct);
            await _songRepo.SaveChangesAsync(ct);
            return song.toDto();
        }


        public async Task DeleteSong(Guid id, CancellationToken ct = default)
        {
            await _songRepo.DeleteAsync(id,ct);
        }

        public async Task<SongResponseDTO> GetSong(Guid id, CancellationToken ct = default)
        {
            var song = await _songRepo.GetByIdAsync(id, ct) ?? throw new NotFoundException("Song",id);
            return song.toDto();
        }


        public async Task<SongResponseDTO> UpdateSong(UpdateSongDTO updateSongDTO, CancellationToken ct = default)
        {
            var song = await _songRepo.GetByIdAsync(updateSongDTO.Id,ct) ?? throw new NotFoundException("Song",updateSongDTO.Id);
            song.Title = updateSongDTO.Title;
            song.Artist = updateSongDTO.Artist;
            song.UpdatedAt = DateTime.UtcNow;

            await _songRepo.SaveChangesAsync(ct);
            return song.toDto();
        }
    }
}
