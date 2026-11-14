using EventManagementSystem.Model.Domain;

namespace EventManagementSystem.Model.DTO
{
    public record AuthResponseDto(
      string Token,
      string Email,
      string FullName,
      bool IsAdmin,
      bool IsSuperAdmin,
      DepartmentType? Department,
      RequesterRole Position,
      YearLevel Level);
}
