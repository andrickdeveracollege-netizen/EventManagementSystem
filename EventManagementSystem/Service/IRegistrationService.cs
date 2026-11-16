using EventManagementSystem.Model.DTO;

namespace EventManagementSystem.Service
{
    public interface IRegistrationService
    {
       
        Task RegisterMemberAsync(int eventId, bool confirmed, Registrant who);
      
        Task RegisterGuestAsync(RegistrationDto dto);
        Task<List<MyRegistrationDto>> GetMineAsync(string email);
        Task CancelAsync(int registrationId, string email);
    }
}
