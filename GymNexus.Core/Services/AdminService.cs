using GymNexus.Core.Contracts;
using GymNexus.Core.Utils;
using GymNexus.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GymNexus.Core.Services;

public class AdminService : IAdminService
{
    private readonly ApplicationDbContext _context;

    public AdminService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> GetAllOrdersCountAsync()
    {
        return await _context.Orders.CountAsync();
    }

    public async Task<int> GetPendingOrdersCountAsync()
    {
        return await _context.Orders.CountAsync(o => o.Status == OrderStatus.Pending.ToString());
    }

    public async Task<int> GetConfirmedOrdersCountAsync()
    {
        return await _context.Orders.CountAsync(o => o.Status == OrderStatus.Confirmed.ToString());
    }

    public async Task<int> GetCompletedOrdersCountAsync()
    {
        return await _context.Orders.CountAsync(o => o.Status == OrderStatus.Completed.ToString() && !o.IsActive);
    }
}