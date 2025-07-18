using FamilyApp.Models.DTOs;

namespace FamilyApp.Services.Interfaces;

public interface ICareTeamService
{
    Task<IEnumerable<CareTeamMemberDto>> GetAllMembersAsync();
    Task<CareTeamMemberDto?> GetMemberByIdAsync(int id);
    Task<IEnumerable<CareTeamMemberDto>> GetMembersByRoleAsync(string role);
    Task<CareTeamMemberDto> CreateMemberAsync(CreateCareTeamMemberDto createMemberDto);
    Task<CareTeamMemberDto?> UpdateMemberAsync(int id, CreateCareTeamMemberDto updateMemberDto);
    Task<bool> DeleteMemberAsync(int id);
}
