namespace EventManagementSystem.Model.Domain
{
    public class Facility
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DepartmentType Department { get; set; }
        public int? Capacity { get; set; }       
        public bool IsOther { get; set; }         
        public ICollection<EventFacility> EventFacilities { get; set; } = new List<EventFacility>();
    }
}
