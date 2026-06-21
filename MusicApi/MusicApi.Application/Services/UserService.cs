using MusicApi.MusicApi.Application.DTOs;
using MusicApi.MusicApi.Application.DTOs.UserDTOs;
using MusicApi.MusicApi.Application.Interfaces;
using MusicApi.MusicApi.Domain.Entities;
using MusicApi.MusicApi.Domain.Exceptions;

namespace MusicApi.MusicApi.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<UserResponseDTO> CreateUser(CreateUserDTO userDto, CancellationToken ct = default)
        {
            var user = User.create(userDto.Name,userDto.Email);
            await _userRepo.AddAsync(user, ct);
            await _userRepo.SaveChangesAsync(ct);
            return user.toDto();
        }

        public async Task<UserResponseDTO> GetUserById(Guid id, CancellationToken ct = default)
        {
            var user = await _userRepo.GetByIdAsync(id) ?? throw new NotFoundException("User",id);
            return user.toDto();
        }
    }
}
