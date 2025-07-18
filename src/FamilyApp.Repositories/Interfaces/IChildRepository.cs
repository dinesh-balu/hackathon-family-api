using FamilyApp.Models.Entities;

namespace FamilyApp.Repositories.Interfaces;

public interface IChildRepository : IRepository<Child>
{
    Task<IEnumerable<Child>> GetChildrenByUserIdAsync(int userId);
    Task<Child?> GetChildWithSessionsAsync(int childId);
}
