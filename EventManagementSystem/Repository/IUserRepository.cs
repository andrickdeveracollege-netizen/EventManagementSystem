using EventManagementSystem.Model.Domain;

namespace EventManagementSystem.Repository
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdAsync(Guid id);
        Task<List<User>> GetAllAsync();
        Task AddAsync(User user);
        Task SaveAsync();
    }
}
