using MusicApi.MusicApi.Application.DTOs.PlaylistDTOs;
using MusicApi.MusicApi.Application.Interfaces;
using MusicApi.MusicApi.Domain.Interfaces;
using MusicApi.MusicApi.Domain.Exceptions;
using MusicApi.MusicApi.Domain.Entities;
using MusicApi.MusicApi.Application.DTOs;

namespace MusicApi.MusicApi.Application.Services
{
    public class PlaylistService : IPlaylistService
    {
        private readonly IPlaylistRepository _playlistRepo;
        private readonly IUserRepository _userRepo;
        private readonly ISongRepository _songRepo;
        public PlaylistService(IPlaylistRepository playlistRepo, IUserRepository userRepo, ISongRepository songRepo)
        {
            _playlistRepo = playlistRepo;
            _userRepo = userRepo;
            _songRepo = songRepo;
        }

        public async Task<PlaylistResponseDTO> AddSongAsync(Guid playlistId, AddSongDTO songDto, CancellationToken ct = default)
        {
            var playlist = await _playlistRepo.GetByIdWithSongsAsync(playlistId,ct) ?? throw new NotFoundException("Playlist",playlistId);
            var song = await _songRepo.GetByIdAsync(songDto.SongId) ?? throw new NotFoundException("Song",songDto.SongId);
            playlist.AddSong(song);
            await _playlistRepo.SaveChangesAsync(ct);

            return playlist.ToDto();

        }

        public async Task<PlaylistResponseDTO> CreatePlaylist(CreatePlaylistDTO createPlaylistDto, CancellationToken ct = default)
        {
            var userExists = await _userRepo.ExistsByIdAsync(createPlaylistDto.userId,ct);
            if (!userExists)
            {
                throw new NotFoundException("User", createPlaylistDto.userId);
            }

            var playlist = PlayList.Create(createPlaylistDto.Name, createPlaylistDto.userId);
            await _playlistRepo.AddAsync(playlist,ct);
            await _userRepo.SaveChangesAsync(ct);
            return playlist.ToDto();
        }

        public async Task DeletePlaylist(Guid playlistId, CancellationToken ct = default)
        {
            var playlist = await _playlistRepo.GetByIdAsync(playlistId, ct) ?? throw new NotFoundException("Playlist", playlistId);
            await _playlistRepo.DeleteAsync(playlistId, ct);
            await _playlistRepo.SaveChangesAsync(ct);
        }

        public async Task<IEnumerable<PlaylistResponseDTO>> GetByUserIdAsync(Guid userId, CancellationToken ct = default)
        {
            var userExists = await _userRepo.ExistsByIdAsync(userId);
            if (!userExists) throw new NotFoundException("User", userId);

            var playlists = await _playlistRepo.GetByUserIdAsync(userId);
            return playlists.Select(p => p.ToDto());
        }

        public async Task<PlaylistResponseDTO> GetPlaylist(Guid playlistId,CancellationToken ct = default)
        {

            var playlist = await _playlistRepo.GetByIdWithSongsAsync(playlistId, ct)
                ?? throw new NotFoundException("Playlist", playlistId);

            return playlist.ToDto();
        }

        public async Task RemoveSongAsync(Guid playlistId, Guid songId, CancellationToken ct = default)
        {
            var playlist = await _playlistRepo.GetByIdWithSongsAsync(playlistId, ct) ?? throw new NotFoundException("Playlist", playlistId);
            playlist.RemoveSong(songId);
            await _playlistRepo.UpdateAsync(playlist, ct);
            await _playlistRepo.SaveChangesAsync(ct);
        }

        public async Task UpdatePlaylist(Guid playlistId, UpdatePlaylistDTO updatePlaylistDto, CancellationToken ct = default)
        {
            var playlist = await _playlistRepo.GetByIdAsync(playlistId, ct) ?? throw new NotFoundException("Playlist", playlistId);
            playlist.Update(updatePlaylistDto.Name);

            await _playlistRepo.UpdateAsync(playlist,ct);
            await _playlistRepo.SaveChangesAsync(ct);
        }
    }
}
