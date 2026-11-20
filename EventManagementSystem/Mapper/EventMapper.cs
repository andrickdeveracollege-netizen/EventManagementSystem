using EventManagementSystem.Model.Domain;
using EventManagementSystem.Model.DTO;

namespace EventManagementSystem.Mapper
{
    public static class EventMapper
    {
        public static EventSummaryDto ToSummary(this CampusEvent e)
        {
            var rows = e.DepartmentRequests.SelectMany(d => d.Facilities).Where(f => f.Facility != null).ToList();

            return new(
                e.Id, e.Title, e.Organization, e.Purpose,
                rows.Select(f => f.FacilityId).Distinct().ToList(),
                rows.Select(f => f.DisplayName()).Distinct().ToList(),
                e.StartTime, e.EndTime, e.TargetAudience, e.AudienceDetail,
                e.Capacity, e.Registrations.Count(r => !r.Cancelled),
                e.IsInstitutional, e.RequiresRegistration, e.Status, e.ShowOnBulletin);
        }

        // Private events still block their venue on the calendar, but without their details.
        public static EventSummaryDto Masked(this EventSummaryDto s) => s with
        {
            Title = "Reserved",
            Organization = "Private event",
            Purpose = string.Empty,
            AudienceDetail = null
        };

        public static EventDetailDto ToDetail(this CampusEvent e) => new(
            e.ToSummary(), e.RequesterName, e.Role,
            e.DepartmentRequests
                .Select(d => new DepartmentStatusDto(FacilityMapper.OfficeName(d.Department), d.Status, d.RejectionReason, d.AdminNote,
                    EquipmentRejected: d.EquipmentRejected
                        || (d.Status == EventStatus.Rejected && d.Facilities.Count == 0 && e.Status != EventStatus.Rejected)))
                .ToList());
    }

}
