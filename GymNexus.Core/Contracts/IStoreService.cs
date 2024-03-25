using GymNexus.Core.Models;

namespace GymNexus.Core.Contracts;

public interface IStoreService
{
    Task<IEnumerable<StoreDto>> GetAllStoresAsync();

    Task<StoreViewDto?> GetStoreByMarketplaceIdAsync(int marketplaceId);
}