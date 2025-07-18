using System.ComponentModel.DataAnnotations;

namespace FamilyApp.Models.Entities;

public class Child
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    public DateTime DateOfBirth { get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public ICollection<TherapySession> TherapySessions { get; set; } = new List<TherapySession>();
}
