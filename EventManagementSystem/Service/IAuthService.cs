using EventManagementSystem.Model.DTO;

namespace EventManagementSystem.Service
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterAccountDto dto);
        Task<AuthResponseDto> LoginAsync(LoginDto dto);
        Task ChangePasswordAsync(string email, ChangePasswordDto dto);
    }
}
