using FamilyApp.Models.Entities;

namespace FamilyApp.Repositories.Interfaces;

public interface ICareTeamRepository : IRepository<CareTeamMember>
{
    Task<IEnumerable<CareTeamMember>> GetMembersByRoleAsync(string role);
    Task<CareTeamMember?> GetByEmailAsync(string email);
}
