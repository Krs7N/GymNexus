using GymNexus.Core.Models;

namespace GymNexus.Core.Contracts;

public interface IMarketplaceService
{
    Task<IEnumerable<MarketplaceDto>> GetAllAsync();
    Task<IEnumerable<MarketplaceViewDto>> GetAllWithStoresAsync();
}