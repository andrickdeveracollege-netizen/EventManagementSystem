using EventManagementSystem.Model.Domain;
using System.ComponentModel.DataAnnotations;

namespace EventManagementSystem.Model.DTO
{
    public class InstitutionalEventDto
    {
        [Required, StringLength(200)] public string Title { get; set; } = "";
        [Required, StringLength(150)] public string Organization { get; set; } = "";
        [Required, StringLength(2000)] public string Purpose { get; set; } = "";
        public YearLevel TargetAudience { get; set; } = YearLevel.All;
        [StringLength(200)] public string? AudienceDetail { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool ShowOnBulletin { get; set; } = true;
        [Required, MinLength(1)] public List<int> FacilityIds { get; set; } = new();
    }
}
