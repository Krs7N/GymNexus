using GymNexus.Core.Contracts;
using GymNexus.Core.Models;
using GymNexus.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace GymNexus.Core.Services;

public class ProfileService : IProfileService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _context;

    public ProfileService(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task<ProfileUpdateResponseDto> UpdateProfileAsync(ProfileUpdateDto profileUpdateDto, ApplicationUser user)
    {
        user.ProfilePictureUrl = profileUpdateDto.ImageUrl;
        await _userManager.UpdateAsync(user);

        await _context.SaveChangesAsync();

        return new ProfileUpdateResponseDto()
        {
            Email = user.Email,
            ImageUrl = user.ProfilePictureUrl,
            Roles = await _userManager.GetRolesAsync(user)
        };
    }
}