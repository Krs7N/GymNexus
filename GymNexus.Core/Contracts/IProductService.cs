using GymNexus.Core.Models;

namespace GymNexus.Core.Contracts;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllAsync(string userId);
    Task ToggleProductLikeByIdAsync(int id, string userId);
    Task<bool> IsCurrentUserLikedProductAsync(int id, string userId);
}