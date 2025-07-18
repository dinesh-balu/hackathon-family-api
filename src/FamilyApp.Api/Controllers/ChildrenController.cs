using Microsoft.AspNetCore.Mvc;
using FamilyApp.Models.DTOs;
using FamilyApp.Services.Interfaces;

namespace FamilyApp.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ChildrenController : ControllerBase
{
    private readonly IChildService _childService;
    private readonly ITherapySessionService _sessionService;
    private readonly ILogger<ChildrenController> _logger;

    public ChildrenController(
        IChildService childService, 
        ITherapySessionService sessionService,
        ILogger<ChildrenController> logger)
    {
        _childService = childService;
        _sessionService = sessionService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ChildDto>>> GetChildren()
    {
        try
        {
            var children = await _childService.GetAllChildrenAsync();
            return Ok(children);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving children");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ChildDto>> GetChild(int id)
    {
        try
        {
            var child = await _childService.GetChildByIdAsync(id);
            if (child == null)
            {
                return NotFound();
            }
            return Ok(child);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving child {ChildId}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}/sessions")]
    public async Task<ActionResult<IEnumerable<TherapySessionDto>>> GetChildSessions(int id)
    {
        try
        {
            var child = await _childService.GetChildByIdAsync(id);
            if (child == null)
            {
                return NotFound();
            }

            var sessions = await _sessionService.GetSessionsByChildIdAsync(id);
            return Ok(sessions);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving sessions for child {ChildId}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public async Task<ActionResult<ChildDto>> CreateChild(CreateChildDto createChildDto)
    {
        try
        {
            var child = await _childService.CreateChildAsync(createChildDto);
            return CreatedAtAction(nameof(GetChild), new { id = child.Id }, child);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating child");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ChildDto>> UpdateChild(int id, CreateChildDto updateChildDto)
    {
        try
        {
            var child = await _childService.UpdateChildAsync(id, updateChildDto);
            if (child == null)
            {
                return NotFound();
            }
            return Ok(child);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating child {ChildId}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteChild(int id)
    {
        try
        {
            var result = await _childService.DeleteChildAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting child {ChildId}", id);
            return StatusCode(500, "Internal server error");
        }
    }
}
