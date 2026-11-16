
using EventManagementSystem.Model.DTO;
using EventManagementSystem.Service;
using EventManagementSystem.Repository;
using EventManagementSystem.Mapper;

namespace EventManagementSystem.Service;

public class EquipmentService : IEquipmentService
{
    private readonly IEquipmentRepository _equipment;
    public EquipmentService(IEquipmentRepository equipment) => _equipment = equipment;

    public async Task<List<EquipmentDto>> GetCatalogAsync() =>
        (await _equipment.GetAllAsync()).Select(e => e.ToDto()).ToList();
}
