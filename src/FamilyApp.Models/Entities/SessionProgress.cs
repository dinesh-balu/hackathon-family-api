using System.ComponentModel.DataAnnotations;

namespace FamilyApp.Models.Entities;

public class SessionProgress
{
    public int Id { get; set; }
    
    public int SessionId { get; set; }
    public TherapySession Session { get; set; } = null!;
    
    [MaxLength(1000)]
    public string? ProgressNotes { get; set; }
    
    public decimal CompletionPercentage { get; set; }
    
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
