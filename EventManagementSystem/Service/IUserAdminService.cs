using EventManagementSystem.Model.DTO;

namespace EventManagementSystem.Service
{
    public interface IUserAdminService
    {
        Task<List<UserDto>> GetUsersAsync();
        Task ResetPasswordAsync(Guid id);
        Task SetPositionAsync(Guid id, SetPositionDto dto);
    }
}
