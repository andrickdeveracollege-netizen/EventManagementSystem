using System.ComponentModel.DataAnnotations;

namespace EventManagementSystem.Model.DTO
{
    public class GuestEventRequestDto : EventRequestDto
    {
        [Required, EmailAddress, StringLength(200)] public string GuestEmail { get; set; } = "";
        [StringLength(30)] public string? Phone { get; set; }
    }
}
