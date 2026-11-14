using EventManagementSystem.Model.Domain;

namespace EventManagementSystem.Model.DTO
{
    public record EquipmentDto(
    int Id,
    string Name,
    DepartmentType Department,
    string ApprovingOffice,
    bool IsOther);
}
