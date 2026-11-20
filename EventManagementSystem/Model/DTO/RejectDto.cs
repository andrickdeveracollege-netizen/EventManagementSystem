using System.ComponentModel.DataAnnotations;

namespace EventManagementSystem.Model.DTO
{
    public class RejectDto
    {
        [Required, StringLength(500)] public string Reason { get; set; } = "";

        // true = refuse only this office's equipment; the venue is approved and the event can go on
        public bool EquipmentOnly { get; set; }
    }
}
