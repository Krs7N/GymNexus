using GymNexus.Core.Contracts;
using GymNexus.Core.Models;
using GymNexus.Core.Utils;
using GymNexus.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using static GymNexus.Infrastructure.Constants.DataConstants;

namespace GymNexus.Core.Services;

public class AdminService : IAdminService
{
    private readonly ApplicationDbContext _context;

    public AdminService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync(ApplicationUser user)
    {
        return await _context.Orders
            .Include(o => o.OrdersDetails)
            .ThenInclude(od => od.Product)
            .AsNoTracking()
            .Where(o => o.IsActive)
            .Select(o => new OrderDto()
            {
                Id = o.Id,
                CreatedBy = user.UserName,
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
}