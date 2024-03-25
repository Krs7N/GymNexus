using GymNexus.Core.Contracts;
using GymNexus.Core.Models;
using GymNexus.Infrastructure.Data;
using GymNexus.Infrastructure.Data.Models;
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
                Category = new CategoryDto()
                {
                    Id = p.CategoryId,
                    Name = p.Category.Name
                },
                Store = new StoreDto()
                {
                    Id = p.StoreId,
                    Name = p.Store.Name,
                }
            })
            .ToListAsync();
    }

    public async Task ToggleProductLikeByIdAsync(int id, string userId)
    {
        var product = await _context.Products
            .Where(p => p.IsActive)
            .Include(p => p.ProductsLikes)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
        {
            throw new InvalidOperationException();
        }

        var like = product.ProductsLikes.FirstOrDefault(pl => pl.UserId == userId);

        if (like == null)
        {
            await _context.ProductsLikes.AddAsync(new ProductLike()
            {
                ProductId = product.Id,
                UserId = userId
            });
        }
        else
        {
            _context.ProductsLikes.Remove(like);
        }

        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsCurrentUserLikedProductAsync(int id, string userId)
    {
        return await _context.ProductsLikes
            .AsNoTracking()
            .AnyAsync(pl => pl.ProductId == id && pl.UserId == userId);
    }

    public async Task<ProductDto?> GetProductByIdAsync(int id, string userId)
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
                Category = new CategoryDto()
                {
                    Id = p.CategoryId,
                    Name = p.Category.Name
                },
                Store = new StoreDto()
                {
                    Id = p.StoreId,
                    Name = p.Store.Name
                }
            })
            .FirstOrDefaultAsync();
    }
}