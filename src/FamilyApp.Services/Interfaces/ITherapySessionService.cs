using FamilyApp.Models.DTOs;

namespace FamilyApp.Services.Interfaces;

public interface ITherapySessionService
{
    Task<IEnumerable<TherapySessionDto>> GetAllSessionsAsync();
    Task<TherapySessionDto?> GetSessionByIdAsync(int id);
    Task<IEnumerable<TherapySessionDto>> GetSessionsByChildIdAsync(int childId);
    Task<IEnumerable<TherapySessionDto>> GetTodaysSessionsAsync();
    Task<TherapySessionDto> CreateSessionAsync(CreateTherapySessionDto createSessionDto);
    Task<TherapySessionDto?> UpdateSessionAsync(int id, CreateTherapySessionDto updateSessionDto);
    Task<bool> UpdateSessionProgressAsync(int sessionId, UpdateSessionProgressDto progressDto);
    Task<bool> DeleteSessionAsync(int id);
}
