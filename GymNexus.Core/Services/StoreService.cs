using GymNexus.Core.Contracts;
using GymNexus.Core.Models;
using GymNexus.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using static GymNexus.Infrastructure.Constants.DataConstants;

namespace GymNexus.Core.Services;

public class StoreService : IStoreService
{
    private readonly ApplicationDbContext _context;

    public StoreService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<StoreDto>> GetAllStoresAsync()
    {
        return await _context.Stores
            .AsNoTracking()
            .Where(s => s.IsActive)
            .Select(s => new StoreDto()
            {
                Name = s.Name,
                Description = s.Description,
                Marketplace = new MarketplaceViewDto()
                {
                    Id = s.MarketplaceId,
                    Name = s.Marketplace.Name
                },
                AverageRating = s.AverageRating,
                CreatedOn = s.CreatedOn.ToString(DateTimeFormat),
                Owner = s.Owner.UserName,
                RatingsCount = s.RatingsCount
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<StoreViewDto?>> GetStoresByMarketplaceIdAsync(int marketplaceId)
    {
        return await _context.Stores
            .AsNoTracking()
            .Where(s => s.IsActive && s.MarketplaceId == marketplaceId)
            .Select(s => new StoreViewDto()
            {
                Id = s.Id,
                Name = s.Name
            })
            .ToListAsync();
    }
}