namespace FamilyApp.Models.DTOs;

public class ChildDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateChildDto
{
    public string Name { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public int UserId { get; set; }
}
