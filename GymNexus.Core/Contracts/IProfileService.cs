using GymNexus.Core.Models;
using GymNexus.Infrastructure.Data;

namespace GymNexus.Core.Contracts;

public interface IProfileService
{
    Task<ProfileUpdateResponseDto> UpdateProfileAsync(ProfileUpdateDto profileUpdateDto, ApplicationUser user);

    Task<IEnumerable<StoreDto>> GetUserStoresAsync(string userId);
}