using EventManagementSystem.Repository;
using EventManagementSystem.Model.Domain;

namespace EventManagementSystem.Repository
{

    public interface IEventRepository
    {
        Task<List<CampusEvent>> GetBulletinAsync(DateTime now);
        Task<List<CampusEvent>> GetCalendarAsync(DateTime from, DateTime to, int? facilityId);
        Task<List<CampusEvent>> GetMineAsync(string requesterEmail);
        Task<List<CampusEvent>> GetForDepartmentAsync(DepartmentType dept);
        Task<CampusEvent?> GetDetailAsync(int id);
        Task<CampusEvent?> GetByIdAsync(int id);
        Task<List<FacilityConflict>> GetConflictsAsync(IEnumerable<int> facilityIds, DateTime start, DateTime end, bool approvedOnly, int? excludeEventId = null);
        Task AddAsync(CampusEvent ev);
        Task SaveAsync();
    }
}
