using Microsoft.EntityFrameworkCore;
using FamilyApp.Database;
using FamilyApp.Models.Entities;
using FamilyApp.Repositories.Interfaces;

namespace FamilyApp.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(FamilyAppDbContext context) : base(context)
    {
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<IEnumerable<User>> GetUsersByRoleAsync(string role)
    {
        return await _dbSet.Where(u => u.Role == role).ToListAsync();
    }

    public override async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _dbSet.Include(u => u.Children).ToListAsync();
    }
}
