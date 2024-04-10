using GymNexus.Core.Contracts;
using GymNexus.Core.Models;
using GymNexus.Core.Utils;
using GymNexus.Infrastructure.Data;
using GymNexus.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;
using static GymNexus.Infrastructure.Constants.DataConstants;

namespace GymNexus.Core.Services;

public class AdminService : IAdminService
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public AdminService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
    {
        return await _context.Orders
            .Include(o => o.OrdersDetails)
            .ThenInclude(od => od.Product)
            .AsNoTracking()
            .Where(o => o.IsActive)
            .Select(o => new OrderDto()
            {
                Id = o.Id,
                CreatedBy = o.Creator.Email,
                CreatedOn = o.CreatedOn.ToString(DateTimeFormat),
                Quantity = o.Quantity,
                Status = o.Status,
                TotalPrice = o.TotalPrice,
                Products = o.OrdersDetails.Select(od => new ProductOrderDto()
                {
                    Name = od.Product.Name,
                    ImageUrl = od.Product.ImageUrl,
                    Category = od.Product.Category.Name,
                    Price = od.Product.Price,
                    Quantity = od.Quantity
                }).ToArray()
            })
            .ToListAsync();
    }

    public async Task<int> GetAllOrdersCountAsync()
    {
        return await _context.Orders.AsNoTracking().CountAsync();
    }

    public async Task<int> GetPendingOrdersCountAsync()
    {
        return await _context.Orders.AsNoTracking().CountAsync(o => o.Status == OrderStatus.Pending.ToString());
    }

    public async Task<int> GetConfirmedOrdersCountAsync()
    {
        return await _context.Orders.AsNoTracking().CountAsync(o => o.Status == OrderStatus.Confirmed.ToString());
    }

    public async Task<int> GetCompletedOrdersCountAsync()
    {
        return await _context.Orders.AsNoTracking().CountAsync(o => o.Status == OrderStatus.Completed.ToString() && !o.IsActive);
    }

    public async Task<string> ChangeOrderStatusAsync(int id, string status)
    {
        if (id < 1)
        {
            throw new InvalidOperationException();
        }

        if (!Enum.IsDefined(typeof(OrderStatus), status) || string.IsNullOrEmpty(status))
        {
            throw new InvalidOperationException("The provided status is invalid");
        }

        var newStatus = status == OrderStatus.Pending.ToString()
            ? OrderStatus.Confirmed.ToString()
            : OrderStatus.Completed.ToString();

        var order = await _context.Orders.FindAsync(id);

        if (order != null && !order.IsActive)
        {
            throw new InvalidOperationException();
        }

        if (newStatus == OrderStatus.Completed.ToString())
        {
            order!.IsActive = false;
        }

        order!.Status = newStatus;

        await _context.SaveChangesAsync();

        return newStatus;
    }

    public async Task<PostPreviewDto?> GetMostLikedPostAsync()
    {
        return await _context.Posts
            .AsNoTracking()
            .Where(p => p.IsActive)
            .OrderByDescending(p => p.PostsLikes.Count())
            .Select(p => new PostPreviewDto()
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content.Length > 50 ? p.Content.Substring(0, 50) + "..." : p.Content,
                Likes = p.PostsLikes.Count()
            })
            .FirstOrDefaultAsync();
    }

    public async Task<PostPreviewDto?> GetMostCommentedPostAsync()
    {
        return await _context.Posts
            .AsNoTracking()
            .Where(p => p.IsActive)
            .OrderByDescending(p => p.Comments.Count(c => c.IsActive))
            .Select(p => new PostPreviewDto()
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content.Length > 50 ? p.Content.Substring(0, 50) + "..." : p.Content,
                Comments = p.Comments.Count(c => c.IsActive)
            })
            .FirstOrDefaultAsync();
    }

    public async Task AddMarketplaceAsync(MarketplaceFormDto marketplaceFormDto)
    {
        var formattedAddress = Uri.EscapeDataString(marketplaceFormDto.Address);
        var url = $"https://nominatim.openstreetmap.org/search?format=json&q={formattedAddress}";

        using var httpClient = new HttpClient();

        httpClient.DefaultRequestHeaders.Add("User-Agent", "GymNexus");

        var response = await httpClient.GetStringAsync(url);
        var geocodeResults = JsonConvert.DeserializeObject<List<GeocodeResult>>(response);

        if (geocodeResults != null && geocodeResults.Count == 0)
        {
            throw new InvalidOperationException("The provided address is invalid");
        }

        var marketplace = new Marketplace()
        {
            Name = marketplaceFormDto.Name,
            Description = marketplaceFormDto.Description,
            Address = marketplaceFormDto.Address,
            Latitude = decimal.Parse(geocodeResults[0].Latitude),
            Longitude = decimal.Parse(geocodeResults[0].Longitude)
        };

        await _context.Marketplaces.AddAsync(marketplace);
        await _context.SaveChangesAsync();
    }
}