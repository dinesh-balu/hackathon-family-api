using FamilyApp.Models.DTOs;

namespace FamilyApp.Services.Interfaces;

public interface IChildService
{
    Task<IEnumerable<ChildDto>> GetAllChildrenAsync();
    Task<ChildDto?> GetChildByIdAsync(int id);
    Task<IEnumerable<ChildDto>> GetChildrenByUserIdAsync(int userId);
    Task<ChildDto> CreateChildAsync(CreateChildDto createChildDto);
    Task<ChildDto?> UpdateChildAsync(int id, CreateChildDto updateChildDto);
    Task<bool> DeleteChildAsync(int id);
}
