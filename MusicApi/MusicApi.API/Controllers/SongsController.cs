using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicApi.MusicApi.Application.DTOs.SongDTOs;
using MusicApi.MusicApi.Application.Interfaces;
using MusicApi.MusicApi.Domain.Entities;

namespace MusicApi.MusicApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {

        private readonly ISongService _songService;
        public SongsController(ISongService songService)
        {
            _songService = songService;
        }

        [HttpGet("{songId:guid}")]
        public async Task<IActionResult> FindById([FromRoute] Guid songId)
        {
            var song = await _songService.GetSong(songId);
            if(song is null) { return NotFound("Song is not found"); }
            return Ok(song);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateSongDTO createSongDto)
        {
            var song = await _songService.CreateSong(createSongDto);
            return CreatedAtAction(nameof(FindById), new { songId = song.Id},song);
        }

        [HttpDelete("{songId:guid}")]
        public async Task<IActionResult> Delete(Guid songId)
        {
            await _songService.DeleteSong(songId);
            return NoContent();
        }

        [HttpPatch("{songId:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid songId,[FromBody] UpdateSongDTO updateSongDto)
        {
            await _songService.UpdateSong(songId,updateSongDto);
            return NoContent();
        }
    }
}
