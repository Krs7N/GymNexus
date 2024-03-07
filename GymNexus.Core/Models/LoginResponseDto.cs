namespace GymNexus.Core.Models;

public class LoginResponseDto
{
    public string Email { get; set; } = string.Empty;

    public string Token { get; set; } = string.Empty;

    public string? ImageUrl { get; set; }

    public IEnumerable<string> Roles { get; set; } = new List<string>();
}