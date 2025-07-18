using Microsoft.EntityFrameworkCore;
using FamilyApp.Database;
using FamilyApp.Models.Entities;
using FamilyApp.Repositories.Interfaces;

namespace FamilyApp.Repositories;

public class TherapySessionRepository : Repository<TherapySession>, ITherapySessionRepository
{
    public TherapySessionRepository(FamilyAppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<TherapySession>> GetSessionsByChildIdAsync(int childId)
    {
        return await _dbSet
            .Include(s => s.Child)
            .Include(s => s.SessionProgresses)
            .Where(s => s.ChildId == childId)
            .OrderByDescending(s => s.Date)
            .ToListAsync();
    }

    public async Task<IEnumerable<TherapySession>> GetTodaysSessionsAsync()
    {
        var today = DateTime.Today;
        var tomorrow = today.AddDays(1);
        
        return await _dbSet
            .Include(s => s.Child)
            .Include(s => s.SessionProgresses)
            .Where(s => s.Date >= today && s.Date < tomorrow)
            .OrderBy(s => s.Date)
            .ToListAsync();
    }

    public async Task<TherapySession?> GetSessionWithProgressAsync(int sessionId)
    {
        return await _dbSet
            .Include(s => s.Child)
            .Include(s => s.SessionProgresses)
            .FirstOrDefaultAsync(s => s.Id == sessionId);
    }
}
