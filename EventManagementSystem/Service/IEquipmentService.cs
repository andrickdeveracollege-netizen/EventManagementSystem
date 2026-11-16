using EventManagementSystem.Model.DTO;

namespace EventManagementSystem.Service;

public interface IEquipmentService
{
    Task<List<EquipmentDto>> GetCatalogAsync();
}