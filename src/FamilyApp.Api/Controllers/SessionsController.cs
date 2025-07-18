using Microsoft.AspNetCore.Mvc;
using FamilyApp.Models.DTOs;
using FamilyApp.Services.Interfaces;

namespace FamilyApp.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class SessionsController : ControllerBase
{
    private readonly ITherapySessionService _sessionService;
    private readonly ILogger<SessionsController> _logger;

    public SessionsController(ITherapySessionService sessionService, ILogger<SessionsController> logger)
    {
        _sessionService = sessionService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TherapySessionDto>>> GetSessions()
    {
        try
        {
            var sessions = await _sessionService.GetAllSessionsAsync();
            return Ok(sessions);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving sessions");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("today")]
    public async Task<ActionResult<IEnumerable<TherapySessionDto>>> GetTodaysSessions()
    {
        try
        {
            var sessions = await _sessionService.GetTodaysSessionsAsync();
            return Ok(sessions);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving today's sessions");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TherapySessionDto>> GetSession(int id)
    {
        try
        {
            var session = await _sessionService.GetSessionByIdAsync(id);
            if (session == null)
            {
                return NotFound();
            }
            return Ok(session);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving session {SessionId}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public async Task<ActionResult<TherapySessionDto>> CreateSession(CreateTherapySessionDto createSessionDto)
    {
        try
        {
            var session = await _sessionService.CreateSessionAsync(createSessionDto);
            return CreatedAtAction(nameof(GetSession), new { id = session.Id }, session);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating session");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TherapySessionDto>> UpdateSession(int id, CreateTherapySessionDto updateSessionDto)
    {
        try
        {
            var session = await _sessionService.UpdateSessionAsync(id, updateSessionDto);
            if (session == null)
            {
                return NotFound();
            }
            return Ok(session);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating session {SessionId}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("{id}/progress")]
    public async Task<IActionResult> UpdateSessionProgress(int id, UpdateSessionProgressDto progressDto)
    {
        try
        {
            var result = await _sessionService.UpdateSessionProgressAsync(id, progressDto);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating session progress {SessionId}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSession(int id)
    {
        try
        {
            var result = await _sessionService.DeleteSessionAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting session {SessionId}", id);
            return StatusCode(500, "Internal server error");
        }
    }
}
