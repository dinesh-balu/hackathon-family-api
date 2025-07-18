using Microsoft.EntityFrameworkCore;
using FamilyApp.Database;
using FamilyApp.Models.Entities;
using FamilyApp.Repositories.Interfaces;

namespace FamilyApp.Repositories;

public class CareTeamRepository : Repository<CareTeamMember>, ICareTeamRepository
{
    public CareTeamRepository(FamilyAppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<CareTeamMember>> GetMembersByRoleAsync(string role)
    {
        return await _dbSet.Where(m => m.Role == role).ToListAsync();
    }

    public async Task<CareTeamMember?> GetByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(m => m.Email == email);
    }
}
