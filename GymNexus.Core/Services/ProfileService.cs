using GymNexus.Core.Contracts;
using GymNexus.Core.Models;
using GymNexus.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
        user.FirstName = profileUpdateDto.FirstName;
        user.LastName = profileUpdateDto.LastName;
        user.ProfilePictureUrl = profileUpdateDto.ImageUrl;
        await _userManager.UpdateAsync(user);

        await _context.SaveChangesAsync();

        return new ProfileUpdateResponseDto()
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            ImageUrl = user.ProfilePictureUrl,
            Roles = await _userManager.GetRolesAsync(user)
        };
    }

    public async Task<IEnumerable<StoreViewDto>> GetUserStoresAsync(string userId)
    {
        return await _context.Stores
            .AsNoTracking()
            .Where(s => s.IsActive && s.OwnerId == userId)
            .Select(s => new StoreViewDto()
            {
                Id = s.Id,
                Name = s.Name
            })
            .ToListAsync();
    }
}