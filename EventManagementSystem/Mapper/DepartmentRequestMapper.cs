using EventManagementSystem.Model.Domain;
using EventManagementSystem.Model.DTO;

namespace EventManagementSystem.Mapper
{
    public static class DepartmentRequestMapper
    {
        public static AdminRequestDto ToAdminDto(this DepartmentRequest d)
        {
            var e = d.CampusEvent!;
            return new(
                d.Id, e.Id, e.Title, e.RequesterName, e.RequesterEmail, e.Organization,
                e.Role, e.TargetAudience, e.AudienceDetail, e.Purpose, e.StartTime, e.EndTime,
                d.Facilities.Select(f => f.DisplayName()).ToList(),
                d.EquipmentItems.Select(i => new AdminEquipmentLineDto(i.Equipment!.Name, i.Quantity, i.Notes)).ToList(),
                d.Status,
                d.EndorsementStoredName != null,
                true,
                d.EndorsementOriginalName,
                d.RejectionReason,
                d.AdminNote,
                e.CreatedAt,
                e.ShowOnBulletin,
                d.EquipmentRejected);
        }
    }
}
