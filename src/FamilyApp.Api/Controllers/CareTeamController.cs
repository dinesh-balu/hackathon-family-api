using Microsoft.AspNetCore.Mvc;
using FamilyApp.Models.DTOs;
using FamilyApp.Services.Interfaces;

namespace FamilyApp.Api.Controllers;

[ApiController]
[Route("api/v1/care-team")]
public class CareTeamController : ControllerBase
{
    private readonly ICareTeamService _careTeamService;
    private readonly ILogger<CareTeamController> _logger;

    public CareTeamController(ICareTeamService careTeamService, ILogger<CareTeamController> logger)
    {
        _careTeamService = careTeamService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CareTeamMemberDto>>> GetCareTeamMembers()
    {
        try
        {
            var members = await _careTeamService.GetAllMembersAsync();
            return Ok(members);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving care team members");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CareTeamMemberDto>> GetCareTeamMember(int id)
    {
        try
        {
            var member = await _careTeamService.GetMemberByIdAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            return Ok(member);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving care team member {MemberId}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public async Task<ActionResult<CareTeamMemberDto>> CreateCareTeamMember(CreateCareTeamMemberDto createMemberDto)
    {
        try
        {
            var member = await _careTeamService.CreateMemberAsync(createMemberDto);
            return CreatedAtAction(nameof(GetCareTeamMember), new { id = member.Id }, member);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating care team member");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CareTeamMemberDto>> UpdateCareTeamMember(int id, CreateCareTeamMemberDto updateMemberDto)
    {
        try
        {
            var member = await _careTeamService.UpdateMemberAsync(id, updateMemberDto);
            if (member == null)
            {
                return NotFound();
            }
            return Ok(member);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating care team member {MemberId}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCareTeamMember(int id)
    {
        try
        {
            var result = await _careTeamService.DeleteMemberAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting care team member {MemberId}", id);
            return StatusCode(500, "Internal server error");
        }
    }
}
