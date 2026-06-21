using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicApi.MusicApi.Application.DTOs.UserDTOs;
using MusicApi.MusicApi.Application.Interfaces;

namespace MusicApi.MusicApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDTO userDto) { 
            var user = await _userService.CreateUser(userDto);
            return CreatedAtAction(nameof(FindById), new { userId = user.Id }, user);
        }

        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> FindById([FromRoute] Guid userId)
        {
            var user = await _userService.GetUserById(userId);
            if (user is null)
                return NotFound();
            return Ok(user);
        }
    }
}
