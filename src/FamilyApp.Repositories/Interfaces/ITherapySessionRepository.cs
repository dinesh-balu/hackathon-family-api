using FamilyApp.Models.Entities;

namespace FamilyApp.Repositories.Interfaces;

public interface ITherapySessionRepository : IRepository<TherapySession>
{
    Task<IEnumerable<TherapySession>> GetSessionsByChildIdAsync(int childId);
    Task<IEnumerable<TherapySession>> GetTodaysSessionsAsync();
    Task<TherapySession?> GetSessionWithProgressAsync(int sessionId);
}
