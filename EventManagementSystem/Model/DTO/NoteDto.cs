using System.ComponentModel.DataAnnotations;

namespace EventManagementSystem.Model.DTO
{
    public class NoteDto
    {
        // Empty text clears the note
        [StringLength(1000)] public string Note { get; set; } = "";
    }
}
