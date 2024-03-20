using GymNexus.Core.Models;

namespace GymNexus.Core.Contracts;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllAsync(string userId);
}