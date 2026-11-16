using EventManagementSystem.Model.DATA;
using EventManagementSystem.Model.Domain;
using EventManagementSystem.Repository;
using Microsoft.EntityFrameworkCore;

namespace EventManagementSystem.Repository;

public class EquipmentRepository : IEquipmentRepository
{
    private readonly AppDBContext _db;
    public EquipmentRepository(AppDBContext db) => _db = db;

    public Task<List<Equipment>> GetAllAsync() =>
        _db.Equipments.AsNoTracking().OrderBy(e => e.Department).ThenBy(e => e.Id).ToListAsync();
}