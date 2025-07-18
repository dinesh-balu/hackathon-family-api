using Microsoft.EntityFrameworkCore;
using FamilyApp.Database;
using FamilyApp.Models.Entities;
using FamilyApp.Repositories.Interfaces;

namespace FamilyApp.Repositories;

public class SessionProgressRepository : Repository<SessionProgress>, ISessionProgressRepository
{
    public SessionProgressRepository(FamilyAppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<SessionProgress>> GetProgressBySessionIdAsync(int sessionId)
    {
        return await _dbSet
            .Where(p => p.SessionId == sessionId)
            .OrderByDescending(p => p.UpdatedAt)
            .ToListAsync();
    }

    public async Task<SessionProgress?> GetLatestProgressBySessionIdAsync(int sessionId)
    {
        return await _dbSet
            .Where(p => p.SessionId == sessionId)
            .OrderByDescending(p => p.UpdatedAt)
            .FirstOrDefaultAsync();
    }
}
