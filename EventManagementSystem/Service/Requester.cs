
using EventManagementSystem.Model.Domain;

namespace EventManagementSystem.Service;

public record Requester(string Email, RequesterRole Role, bool IsGuest, string? Phone = null);
