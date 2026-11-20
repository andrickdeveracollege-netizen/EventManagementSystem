using EventManagementSystem.Model.Domain;

namespace EventManagementSystem.Model.DTO
{
    public record DepartmentStatusDto(string Office, EventStatus Status, string? RejectionReason, string? Note, bool EquipmentRejected);
}
