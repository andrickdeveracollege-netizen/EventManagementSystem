using EventManagementSystem.Model.Domain;

namespace EventManagementSystem.Repository
{
    public interface IRegistrationRepository
    {
        Task<bool> ExistsAsync(int eventId, string email);
        Task<EventRegistration?> GetAsync(int eventId, string email);
        Task<EventRegistration?> GetByIdAsync(int id);
        Task<int> CountActiveAsync(int eventId);
        Task<List<EventRegistration>> GetByEmailAsync(string email);
        Task AddAsync(EventRegistration registration);
        Task SaveAsync();
    }
}
