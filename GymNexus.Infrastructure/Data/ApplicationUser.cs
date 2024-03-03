using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static GymNexus.Infrastructure.Constants.DataConstants;

namespace GymNexus.Infrastructure.Data;

public class ApplicationUser : IdentityUser
{
    [MaxLength(ProfilePictureMaxLength)]
    public string? ProfilePicture { get; set; }
}