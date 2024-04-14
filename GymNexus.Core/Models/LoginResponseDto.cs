namespace GymNexus.Core.Models;

public class LoginResponseDto : LoginResponsePartialDto
{
    public string Token { get; set; } = string.Empty;
}