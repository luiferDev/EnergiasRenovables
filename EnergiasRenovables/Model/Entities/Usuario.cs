namespace EnergiasRenovables.Model.Entities;

public class Usuario
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string? UserName { get; set; }
    public required string Password { get; set; }
    public required string? Email { get; set; }
    public required string Role { get; set; }
}