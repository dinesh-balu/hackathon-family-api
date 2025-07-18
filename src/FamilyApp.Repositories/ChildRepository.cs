using Microsoft.EntityFrameworkCore;
using FamilyApp.Database;
using FamilyApp.Models.Entities;
using FamilyApp.Repositories.Interfaces;

namespace FamilyApp.Repositories;

public class ChildRepository : Repository<Child>, IChildRepository
{
    public ChildRepository(FamilyAppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Child>> GetChildrenByUserIdAsync(int userId)
    {
        return await _dbSet.Where(c => c.UserId == userId).ToListAsync();
    }

    public async Task<Child?> GetChildWithSessionsAsync(int childId)
    {
        return await _dbSet
            .Include(c => c.TherapySessions)
            .ThenInclude(s => s.SessionProgresses)
            .FirstOrDefaultAsync(c => c.Id == childId);
    }

    public override async Task<IEnumerable<Child>> GetAllAsync()
    {
        return await _dbSet.Include(c => c.User).ToListAsync();
    }
}
