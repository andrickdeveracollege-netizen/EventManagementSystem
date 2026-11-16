using EventManagementSystem.Model.Domain;

namespace EventManagementSystem.Model.DTO
{
    public class DepartmentRequestInputDto
    {
        public DepartmentType Department { get; set; }
        public List<int> FacilityIds { get; set; } = new();
        public string? OtherFacilityName { get; set; }
        public List<EquipmentRequestItemDto> Equipment { get; set; } = new();
        public IFormFile? Endorsement { get; set; }
    }
}
