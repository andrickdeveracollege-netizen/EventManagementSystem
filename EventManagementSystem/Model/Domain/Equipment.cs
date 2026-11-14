namespace EventManagementSystem.Model.Domain
{
    public class Equipment
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DepartmentType Department { get; set; }
        public bool IsOther { get; set; }
    }
}
