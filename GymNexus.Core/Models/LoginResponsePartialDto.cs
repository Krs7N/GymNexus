namespace GymNexus.Core.Models;

public class LoginResponsePartialDto
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string Email { get; set; } = string.Empty;

    public string? ImageUrl { get; set; }

    public IEnumerable<string> Roles { get; set; } = new List<string>();

    public bool IsExternal { get; set; } = false;
}