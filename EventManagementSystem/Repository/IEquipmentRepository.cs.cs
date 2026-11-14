using EventManagementSystem.Model.Domain;

namespace EventManagementSystem.Repository
{
    namespace EventManagement.Repository;

    public interface IEquipmentRepository
    {
        Task<List<Equipment>> GetAllAsync();
    }
}
