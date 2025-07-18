using FamilyApp.Models.DTOs;

namespace FamilyApp.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserDto?> GetUserByIdAsync(int id);
    Task<UserDto?> GetUserByEmailAsync(string email);
    Task<UserDto> CreateUserAsync(CreateUserDto createUserDto);
    Task<UserDto?> UpdateUserAsync(int id, CreateUserDto updateUserDto);
    Task<bool> DeleteUserAsync(int id);
}
