using EventManagementSystem.Model.DATA;
using EventManagementSystem.Model.Domain;
using Microsoft.EntityFrameworkCore;
using EventManagementSystem.Repository;

namespace EventManagementSystem.Repository;

public class UserRepository : IUserRepository
{
    private readonly AppDBContext _db;
    public UserRepository(AppDBContext db) => _db = db;

    public Task<User?> GetByEmailAsync(string email) =>
        _db.Users.FirstOrDefaultAsync(u => u.Email == email);

    public Task<User?> GetByIdAsync(Guid id) =>
        _db.Users.FirstOrDefaultAsync(u => u.Id == id);

    public Task<List<User>> GetAllAsync() =>
        _db.Users.AsNoTracking().OrderBy(u => u.Role).ThenBy(u => u.Email).ToListAsync();

    public async Task AddAsync(User user)
    {
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
    }

    public Task SaveAsync() => _db.SaveChangesAsync();
}
