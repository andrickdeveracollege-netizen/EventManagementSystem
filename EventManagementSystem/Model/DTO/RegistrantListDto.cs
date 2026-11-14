namespace EventManagementSystem.Model.DTO
{
    public record RegistrantListDto(int EventId, string Title, DateTime StartTime, int Capacity, List<RegistrantRowDto> Rows);
}
