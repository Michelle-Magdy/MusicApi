using MusicApi.MusicApi.Application.DTOs.UserDTOs;


namespace MusicApi.MusicApi.Application.Interfaces
{
    public interface IUserService
    {

        Task<UserResponseDTO?> GetUserById(Guid id , CancellationToken ct = default);
        Task<UserResponseDTO> CreateUser(CreateUserDTO user, CancellationToken ct = default);

    }
}
