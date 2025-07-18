using FamilyApp.Models.Entities;

namespace FamilyApp.Repositories.Interfaces;

public interface ISessionProgressRepository : IRepository<SessionProgress>
{
    Task<IEnumerable<SessionProgress>> GetProgressBySessionIdAsync(int sessionId);
    Task<SessionProgress?> GetLatestProgressBySessionIdAsync(int sessionId);
}
