using FamilyApp.Models.DTOs;
using FamilyApp.Models.Entities;
using FamilyApp.Repositories.Interfaces;
using FamilyApp.Services.Interfaces;

namespace FamilyApp.Services;

public class ChildService : IChildService
{
    private readonly IChildRepository _childRepository;
    private readonly IUserRepository _userRepository;

    public ChildService(IChildRepository childRepository, IUserRepository userRepository)
    {
        _childRepository = childRepository;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<ChildDto>> GetAllChildrenAsync()
    {
        var children = await _childRepository.GetAllAsync();
        return children.Select(MapToDto);
    }

    public async Task<ChildDto?> GetChildByIdAsync(int id)
    {
        var child = await _childRepository.GetByIdAsync(id);
        return child != null ? MapToDto(child) : null;
    }

    public async Task<IEnumerable<ChildDto>> GetChildrenByUserIdAsync(int userId)
    {
        var children = await _childRepository.GetChildrenByUserIdAsync(userId);
        return children.Select(MapToDto);
    }

    public async Task<ChildDto> CreateChildAsync(CreateChildDto createChildDto)
    {
        var userExists = await _userRepository.ExistsAsync(createChildDto.UserId);
        if (!userExists)
        {
            throw new InvalidOperationException("User does not exist");
        }

        var child = new Child
        {
            Name = createChildDto.Name,
            DateOfBirth = createChildDto.DateOfBirth,
            UserId = createChildDto.UserId
        };

        var createdChild = await _childRepository.AddAsync(child);
        return MapToDto(createdChild);
    }

    public async Task<ChildDto?> UpdateChildAsync(int id, CreateChildDto updateChildDto)
    {
        var child = await _childRepository.GetByIdAsync(id);
        if (child == null) return null;

        var userExists = await _userRepository.ExistsAsync(updateChildDto.UserId);
        if (!userExists)
        {
            throw new InvalidOperationException("User does not exist");
        }

        child.Name = updateChildDto.Name;
        child.DateOfBirth = updateChildDto.DateOfBirth;
        child.UserId = updateChildDto.UserId;

        await _childRepository.UpdateAsync(child);
        return MapToDto(child);
    }

    public async Task<bool> DeleteChildAsync(int id)
    {
        var child = await _childRepository.GetByIdAsync(id);
        if (child == null) return false;

        await _childRepository.DeleteAsync(child);
        return true;
    }

    private static ChildDto MapToDto(Child child)
    {
        return new ChildDto
        {
            Id = child.Id,
            Name = child.Name,
            DateOfBirth = child.DateOfBirth,
            UserId = child.UserId,
            CreatedAt = child.CreatedAt
        };
    }
}
