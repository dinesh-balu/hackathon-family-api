namespace FamilyApp.Models.DTOs;

public class TherapySessionDto
{
    public int Id { get; set; }
    public int ChildId { get; set; }
    public string ChildName { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public int Duration { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateTherapySessionDto
{
    public int ChildId { get; set; }
    public DateTime Date { get; set; }
    public int Duration { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Notes { get; set; }
}

public class UpdateSessionProgressDto
{
    public string? ProgressNotes { get; set; }
    public decimal CompletionPercentage { get; set; }
}
