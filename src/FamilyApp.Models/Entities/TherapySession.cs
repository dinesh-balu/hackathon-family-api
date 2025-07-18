using System.ComponentModel.DataAnnotations;

namespace FamilyApp.Models.Entities;

public class TherapySession
{
    public int Id { get; set; }
    
    public int ChildId { get; set; }
    public Child Child { get; set; } = null!;
    
    public DateTime Date { get; set; }
    
    public int Duration { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Status { get; set; } = string.Empty;
    
    [MaxLength(1000)]
    public string? Notes { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public ICollection<SessionProgress> SessionProgresses { get; set; } = new List<SessionProgress>();
}
