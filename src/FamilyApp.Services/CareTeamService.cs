using FamilyApp.Models.DTOs;
using FamilyApp.Models.Entities;
using FamilyApp.Repositories.Interfaces;
using FamilyApp.Services.Interfaces;

namespace FamilyApp.Services;

public class CareTeamService : ICareTeamService
{
    private readonly ICareTeamRepository _careTeamRepository;

    public CareTeamService(ICareTeamRepository careTeamRepository)
    {
        _careTeamRepository = careTeamRepository;
    }

    public async Task<IEnumerable<CareTeamMemberDto>> GetAllMembersAsync()
    {
        var members = await _careTeamRepository.GetAllAsync();
        return members.Select(MapToDto);
    }

    public async Task<CareTeamMemberDto?> GetMemberByIdAsync(int id)
    {
        var member = await _careTeamRepository.GetByIdAsync(id);
        return member != null ? MapToDto(member) : null;
    }

    public async Task<IEnumerable<CareTeamMemberDto>> GetMembersByRoleAsync(string role)
    {
        var members = await _careTeamRepository.GetMembersByRoleAsync(role);
        return members.Select(MapToDto);
    }

    public async Task<CareTeamMemberDto> CreateMemberAsync(CreateCareTeamMemberDto createMemberDto)
    {
        var existingMember = await _careTeamRepository.GetByEmailAsync(createMemberDto.Email);
        if (existingMember != null)
        {
            throw new InvalidOperationException("Care team member with this email already exists");
        }

        var member = new CareTeamMember
        {
            Name = createMemberDto.Name,
            Role = createMemberDto.Role,
            Email = createMemberDto.Email,
            Phone = createMemberDto.Phone
        };

        var createdMember = await _careTeamRepository.AddAsync(member);
        return MapToDto(createdMember);
    }

    public async Task<CareTeamMemberDto?> UpdateMemberAsync(int id, CreateCareTeamMemberDto updateMemberDto)
    {
        var member = await _careTeamRepository.GetByIdAsync(id);
        if (member == null) return null;

        member.Name = updateMemberDto.Name;
        member.Role = updateMemberDto.Role;
        member.Email = updateMemberDto.Email;
        member.Phone = updateMemberDto.Phone;

        await _careTeamRepository.UpdateAsync(member);
        return MapToDto(member);
    }

    public async Task<bool> DeleteMemberAsync(int id)
    {
        var member = await _careTeamRepository.GetByIdAsync(id);
        if (member == null) return false;

        await _careTeamRepository.DeleteAsync(member);
        return true;
    }

    private static CareTeamMemberDto MapToDto(CareTeamMember member)
    {
        return new CareTeamMemberDto
        {
            Id = member.Id,
            Name = member.Name,
            Role = member.Role,
            Email = member.Email,
            Phone = member.Phone,
            CreatedAt = member.CreatedAt
        };
    }
}
