using EventManagementSystem.Model.Domain;

namespace EventManagementSystem.Repository
{
    public interface IDepartmentRequestRepository
    {
        Task<List<DepartmentRequest>> GetByDepartmentAsync(DepartmentType dept, EventStatus status);
        Task<DepartmentRequest?> GetByIdAsync(int id);
        Task SaveAsync();
    }
}
