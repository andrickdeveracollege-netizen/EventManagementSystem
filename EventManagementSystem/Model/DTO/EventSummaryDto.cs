using EventManagementSystem.Model.Domain;

namespace EventManagementSystem.Model.DTO
{
    public record EventSummaryDto(
    int Id,
    string Title,
    string Organization,
    string Purpose,
    List<int> FacilityIds,
    List<string> FacilityNames,
    DateTime StartTime,
    DateTime EndTime,
    YearLevel TargetAudience,
    string? AudienceDetail,
    int Capacity,
    int Registered,
    bool IsInstitutional,
    bool RequiresRegistration,
    EventStatus Status);
}
