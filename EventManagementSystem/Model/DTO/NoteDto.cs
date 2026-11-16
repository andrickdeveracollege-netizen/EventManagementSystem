using System.ComponentModel.DataAnnotations;

namespace EventManagementSystem.Model.DTO
{
    public class NoteDto
    {
        [StringLength(1000)] public string Note { get; set; } = "";
    }
}
