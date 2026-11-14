using EventManagementSystem.Model.Domain;

namespace EventManagementSystem.Model.DTO
{
    public record FacilityDto(
    int Id,
    string Name,
    DepartmentType Department,
    string ApprovingOffice,
    bool EndorsementOptional,
    int? Capacity,
    bool IsOther);
}
