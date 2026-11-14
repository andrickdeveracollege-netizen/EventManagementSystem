using EventManagementSystem.Model.Domain;
using System.ComponentModel.DataAnnotations;

namespace EventManagementSystem.Model.DTO
{
    public record MyRegistrationDto(
    int Id,
    int EventId,
    string EventTitle,
    DateTime StartTime,
    YearLevel YearLevel,
    bool Cancelled);
}
