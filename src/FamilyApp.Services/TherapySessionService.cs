using FamilyApp.Models.DTOs;
using FamilyApp.Models.Entities;
using FamilyApp.Repositories.Interfaces;
using FamilyApp.Services.Interfaces;

namespace FamilyApp.Services;

public class TherapySessionService : ITherapySessionService
{
    private readonly ITherapySessionRepository _sessionRepository;
    private readonly IChildRepository _childRepository;
    private readonly ISessionProgressRepository _progressRepository;

    public TherapySessionService(
        ITherapySessionRepository sessionRepository,
        IChildRepository childRepository,
        ISessionProgressRepository progressRepository)
    {
        _sessionRepository = sessionRepository;
        _childRepository = childRepository;
        _progressRepository = progressRepository;
    }

    public async Task<IEnumerable<TherapySessionDto>> GetAllSessionsAsync()
    {
        var sessions = await _sessionRepository.GetAllAsync();
        return sessions.Select(MapToDto);
    }

    public async Task<TherapySessionDto?> GetSessionByIdAsync(int id)
    {
        var session = await _sessionRepository.GetSessionWithProgressAsync(id);
        return session != null ? MapToDto(session) : null;
    }

    public async Task<IEnumerable<TherapySessionDto>> GetSessionsByChildIdAsync(int childId)
    {
        var sessions = await _sessionRepository.GetSessionsByChildIdAsync(childId);
        return sessions.Select(MapToDto);
    }

    public async Task<IEnumerable<TherapySessionDto>> GetTodaysSessionsAsync()
    {
        var sessions = await _sessionRepository.GetTodaysSessionsAsync();
        return sessions.Select(MapToDto);
    }

    public async Task<TherapySessionDto> CreateSessionAsync(CreateTherapySessionDto createSessionDto)
    {
        var childExists = await _childRepository.ExistsAsync(createSessionDto.ChildId);
        if (!childExists)
        {
            throw new InvalidOperationException("Child does not exist");
        }

        var session = new TherapySession
        {
            ChildId = createSessionDto.ChildId,
            Date = createSessionDto.Date,
            Duration = createSessionDto.Duration,
            Status = createSessionDto.Status,
            Notes = createSessionDto.Notes
        };

        var createdSession = await _sessionRepository.AddAsync(session);
        var sessionWithChild = await _sessionRepository.GetSessionWithProgressAsync(createdSession.Id);
        return MapToDto(sessionWithChild!);
    }

    public async Task<TherapySessionDto?> UpdateSessionAsync(int id, CreateTherapySessionDto updateSessionDto)
    {
        var session = await _sessionRepository.GetByIdAsync(id);
        if (session == null) return null;

        var childExists = await _childRepository.ExistsAsync(updateSessionDto.ChildId);
        if (!childExists)
        {
            throw new InvalidOperationException("Child does not exist");
        }

        session.ChildId = updateSessionDto.ChildId;
        session.Date = updateSessionDto.Date;
        session.Duration = updateSessionDto.Duration;
        session.Status = updateSessionDto.Status;
        session.Notes = updateSessionDto.Notes;

        await _sessionRepository.UpdateAsync(session);
        var updatedSession = await _sessionRepository.GetSessionWithProgressAsync(id);
        return MapToDto(updatedSession!);
    }

    public async Task<bool> UpdateSessionProgressAsync(int sessionId, UpdateSessionProgressDto progressDto)
    {
        var sessionExists = await _sessionRepository.ExistsAsync(sessionId);
        if (!sessionExists) return false;

        var progress = new SessionProgress
        {
            SessionId = sessionId,
            ProgressNotes = progressDto.ProgressNotes,
            CompletionPercentage = progressDto.CompletionPercentage
        };

        await _progressRepository.AddAsync(progress);
        return true;
    }

    public async Task<bool> DeleteSessionAsync(int id)
    {
        var session = await _sessionRepository.GetByIdAsync(id);
        if (session == null) return false;

        await _sessionRepository.DeleteAsync(session);
        return true;
    }

    private static TherapySessionDto MapToDto(TherapySession session)
    {
        return new TherapySessionDto
        {
            Id = session.Id,
            ChildId = session.ChildId,
            ChildName = session.Child?.Name ?? string.Empty,
            Date = session.Date,
            Duration = session.Duration,
            Status = session.Status,
            Notes = session.Notes,
            CreatedAt = session.CreatedAt
        };
    }
}
