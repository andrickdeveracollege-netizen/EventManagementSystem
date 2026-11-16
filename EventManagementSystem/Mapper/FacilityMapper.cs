using EventManagementSystem.Model.Domain;
using EventManagementSystem.Model.DTO;

namespace EventManagementSystem.Mapper
{
    public static class FacilityMapper
    {
        public static string OfficeName(DepartmentType d) => d switch
        {
            DepartmentType.Auxiliary => "Auxiliary",
            DepartmentType.ITMC => "ITMC (Technical)",
            DepartmentType.College => "College (Ms. Judith)",
            DepartmentType.TVET => "TVET (Ms. Chavez)",
            DepartmentType.Library => "Library",
            _ => d.ToString()
        };

        public static FacilityDto ToDto(this Facility f) => new(
            f.Id, f.Name, f.Department, OfficeName(f.Department),
            f.Department == DepartmentType.College, f.Capacity, f.IsOther);

        public static string DisplayName(this EventFacility ef) =>
            ef.Facility is { IsOther: true } && !string.IsNullOrWhiteSpace(ef.OtherName)
                ? $"Others: {ef.OtherName}"
                : ef.Facility?.Name ?? "";
    }

}
