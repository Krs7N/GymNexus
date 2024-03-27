using GymNexus.Core.Contracts;
using GymNexus.Core.Models;
using GymNexus.Infrastructure.Data;
using GymNexus.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GymNexus.Core.Services;

public class MarketplaceService : IMarketplaceService
{
    private readonly ApplicationDbContext _context;

    public MarketplaceService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MarketplaceDto>> GetAllAsync()
    {
        return await _context.Marketplaces
            .AsNoTracking()
            .Where(m => m.IsActive)
            .Select(m => new MarketplaceDto()
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                Address = m.Address,
                Latitude = m.Latitude,
                Longitude = m.Longitude
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<MarketplaceViewDto>> GetAllWithStoresAsync()
    {
        return await _context.Marketplaces
            .AsNoTracking()
            .Where(m => m.IsActive && m.Stores.Any())
            .Select(m => new MarketplaceViewDto()
            {
                Id = m.Id,
                Name = m.Name
            })
            .ToListAsync();
    }
}