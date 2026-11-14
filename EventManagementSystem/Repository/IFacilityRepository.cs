using EventManagementSystem.Model.Domain;

namespace EventManagementSystem.Repository
{
    public interface IFacilityRepository
    {
        Task<List<Facility>> GetAllAsync();
        Task<Facility?> GetByIdAsync(int id);
    }
}
