using GymNexus.Core.Contracts;
using GymNexus.Core.Models;
using GymNexus.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GymNexus.Core.Services;

public class NomenclatureService : INomenclatureService
{
    private readonly ApplicationDbContext _context;

    public NomenclatureService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CategoryDto>> GetProductCategoriesAsync()
    {
        return await _context.Categories
            .AsNoTracking()
            .Select(c => new CategoryDto()
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToListAsync();
    }
}