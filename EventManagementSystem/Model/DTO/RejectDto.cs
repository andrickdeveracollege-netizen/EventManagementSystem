using System.ComponentModel.DataAnnotations;

namespace EventManagementSystem.Model.DTO
{
    public class RejectDto
    {
        [Required, StringLength(500)] public string Reason { get; set; } = "";
    }
}
