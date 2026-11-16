
using EventManagementSystem.Model.Domain;

namespace EventManagementSystem.Service;

public record Registrant(string Email, string FullName, RequesterRole Role, YearLevel Level);
