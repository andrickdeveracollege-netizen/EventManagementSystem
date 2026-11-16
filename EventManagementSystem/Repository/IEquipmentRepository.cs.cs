using EventManagementSystem.Model.Domain;

namespace EventManagementSystem.Repository
{

    public interface IEquipmentRepository
    {
        Task<List<Equipment>> GetAllAsync();
    }
}
