namespace EventManagementSystem.Model.Domain;

public class CampusEvent
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;

    public string RequesterName { get; set; } = string.Empty;
    public string RequesterEmail { get; set; } = string.Empty;
    public string? RequesterPhone { get; set; }               
    public string Organization { get; set; } = string.Empty;  
    public RequesterRole Role { get; set; }
    public YearLevel TargetAudience { get; set; }
    public string? AudienceDetail { get; set; }              
    public string Purpose { get; set; } = string.Empty;
    public int Capacity { get; set; }                         

    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public bool IsGuest { get; set; }                          
    public bool IsInstitutional { get; set; }                
    public bool RequiresRegistration { get; set; } = true;     
    public string ManageCode { get; set; } = string.Empty;     

    public EventStatus Status { get; set; } = EventStatus.Pending;
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public ICollection<DepartmentRequest> DepartmentRequests { get; set; } = new List<DepartmentRequest>();
    public ICollection<EventRegistration> Registrations { get; set; } = new List<EventRegistration>();
}