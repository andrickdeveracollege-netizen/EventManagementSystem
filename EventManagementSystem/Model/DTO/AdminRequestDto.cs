using EventManagementSystem.Model.Domain;

namespace EventManagementSystem.Model.DTO
{
    public record AdminRequestDto(
        int Id,
        int EventId,
        string Title,
        string RequesterName,
        string RequesterEmail,
        string Organization,
        RequesterRole Role,
        YearLevel TargetAudience,
        string? AudienceDetail,
        string Purpose,
        DateTime StartTime,
        DateTime EndTime,
        List<string> Facilities,
        List<AdminEquipmentLineDto> Equipment,
        EventStatus Status,
        bool HasEndorsement,
        bool EndorsementOptional,
        string? EndorsementFileName,
        string? RejectionReason,
        string? Note,
        DateTime CreatedAt,
        bool ShowOnBulletin,
        bool EquipmentRejected);

}
