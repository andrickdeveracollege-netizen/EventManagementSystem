using EventManagementSystem.Model.Domain;

namespace EventManagementSystem.Mapper
{
    public static class EquipmentMapper
    {
        public static EquipmentDto ToDto(this Equipment e) => new(
            e.Id, e.Name, e.Department, FacilityMapper.OfficeName(e.Department), e.IsOther);
    }
}

