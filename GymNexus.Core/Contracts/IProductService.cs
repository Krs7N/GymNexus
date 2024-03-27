using GymNexus.Core.Models;
using GymNexus.Infrastructure.Data;
using GymNexus.Infrastructure.Data.Models;

namespace GymNexus.Core.Contracts;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllAsync(string userId);
    Task ToggleProductLikeByIdAsync(int id, string userId);
    Task<bool> IsCurrentUserLikedProductAsync(int id, string userId);
    Task<ProductDto?> GetProductByIdAsync(int id, string userId);
    Task<ProductFormDto> UpdateProductByIdAsync(int id, ProductFormDto productFormDto, ApplicationUser user);
    Task<ProductFormDto> DeleteProductByIdAsync(int id);
    Task<ProductFormDto> AddProductAsync(ProductFormDto productModel, ApplicationUser user);
}