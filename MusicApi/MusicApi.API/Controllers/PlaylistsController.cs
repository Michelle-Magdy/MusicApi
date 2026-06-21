
using Microsoft.AspNetCore.Mvc;
using MusicApi.MusicApi.Application.DTOs.PlaylistDTOs;
using MusicApi.MusicApi.Application.Interfaces;

namespace MusicApi.MusicApi.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlaylistsController : ControllerBase
    {
        private readonly IPlaylistService _playlistService;

        public PlaylistsController(IPlaylistService playlistService)
        {
            _playlistService = playlistService;
        }

        [HttpPost]
        public async Task<ActionResult<PlaylistResponseDTO>> CreatePlaylist([FromBody] CreatePlaylistDTO createPlaylistDto, CancellationToken ct = default)
        {
            var playlist = await _playlistService.CreatePlaylist(createPlaylistDto, ct);
            return CreatedAtAction(nameof(GetPlaylist), new { playlistId = playlist.Id }, playlist);
        }

        [HttpGet("{playlistId:guid}")]
        public async Task<ActionResult<PlaylistResponseDTO>> GetPlaylist(Guid playlistId, CancellationToken ct = default)
        {
            var playlist = await _playlistService.GetPlaylist(playlistId, ct);
            return Ok(playlist);
        }

        [HttpGet("user/{userId:guid}")]
        public async Task<ActionResult<IEnumerable<PlaylistResponseDTO>>> GetByUserId(Guid userId, CancellationToken ct = default)
        {
            var playlists = await _playlistService.GetByUserIdAsync(userId, ct);
            return Ok(playlists);
        }

        [HttpPut("{playlistId:guid}")]
        public async Task<IActionResult> UpdatePlaylist(Guid playlistId, [FromBody] UpdatePlaylistDTO updatePlaylistDto, CancellationToken ct = default)
        {
            await _playlistService.UpdatePlaylist(playlistId, updatePlaylistDto, ct);
            return NoContent();
        }

        [HttpDelete("{playlistId:guid}")]
        public async Task<IActionResult> DeletePlaylist(Guid playlistId, CancellationToken ct = default)
        {
            await _playlistService.DeletePlaylist(playlistId, ct);
            return NoContent();
        }

        [HttpPost("{playlistId:guid}/songs")]
        public async Task<ActionResult<PlaylistResponseDTO>> AddSong(Guid playlistId, [FromBody] AddSongDTO songDto, CancellationToken ct = default)
        {
            var playlist = await _playlistService.AddSongAsync(playlistId, songDto, ct);
            return Ok(playlist);
        }

        [HttpDelete("{playlistId:guid}/songs/{songId:guid}")]
        public async Task<IActionResult> RemoveSong(Guid playlistId, Guid songId, CancellationToken ct = default)
        {
            await _playlistService.RemoveSongAsync(playlistId, songId, ct);
            return NoContent();
        }
    }
}