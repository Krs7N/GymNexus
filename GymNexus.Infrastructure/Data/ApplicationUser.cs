using Microsoft.AspNetCore.Identity;

namespace GymNexus.Infrastructure.Data;

public class ApplicationUser : IdentityUser
{
    public byte[]? ProfilePicture { get; set; }
}