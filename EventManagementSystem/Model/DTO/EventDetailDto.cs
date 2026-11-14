using EventManagementSystem.Model.Domain;

namespace EventManagementSystem.Model.DTO
{
    public record EventDetailDto(
    EventSummaryDto Event,
    string RequesterName,
    RequesterRole Role,
    List<DepartmentStatusDto> Departments);
}
