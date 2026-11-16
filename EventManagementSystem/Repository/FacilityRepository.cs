using EventManagementSystem.Model.DATA;
using EventManagementSystem.Model.Domain;
using EventManagementSystem.Repository;
using Microsoft.EntityFrameworkCore;

namespace EventManagementSystem.Repository;

public class FacilityRepository : IFacilityRepository
{
    private readonly AppDBContext _db;
    public FacilityRepository(AppDBContext db) => _db = db;

    public Task<List<Facility>> GetAllAsync() =>
        _db.Facilities.AsNoTracking().OrderBy(f => f.Department).ThenBy(f => f.Id).ToListAsync();

    public Task<Facility?> GetByIdAsync(int id) => _db.Facilities.FirstOrDefaultAsync(f => f.Id == id);
}