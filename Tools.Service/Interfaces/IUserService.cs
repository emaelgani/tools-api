using Tools.Service.DTOs.User;

namespace Tools.Service.Interfaces
{
    public interface IUserService
    {
        public Task Register(UserDTO userDto);
        public Task<UserResponseDTO> Login(UserDTO userDto);
        public Task<UserResponseDTO?> CheckToken();
    }
}
