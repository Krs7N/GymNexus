using GymNexus.Core.Contracts;
using GymNexus.Core.Models;
using GymNexus.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using static GymNexus.Infrastructure.Constants.DataConstants;

namespace GymNexus.Core.Services;

public class ProductService : IProductService
{
    private readonly ApplicationDbContext _context;

    public ProductService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync(string userId)
    {
        return await _context.Products
            .AsNoTracking()
            .Where(p => p.IsActive)
            .Select(p => new ProductDto()
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                Price = p.Price,
                CreatedOn = p.CreatedOn.ToString(DateTimeFormat),
                Marketplace = p.Store.Marketplace.Name,
                Likes = p.ProductsLikes.Count(pl => pl.ProductId == p.Id),
                IsLikedByCurrentUser = p.ProductsLikes.Any(pl => pl.UserId == userId),
                Category = p.Category.Name,
                Store = p.Store.Name
            })
            .ToListAsync();
    }
}