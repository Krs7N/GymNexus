using GymNexus.Core.Models;

namespace GymNexus.Core.Contracts;

public interface IStoreService
{
    Task<IEnumerable<StoreDto>> GetAllStoresAsync();

    Task<IEnumerable<StoreViewDto?>> GetStoresByMarketplaceIdAsync(int marketplaceId);
}