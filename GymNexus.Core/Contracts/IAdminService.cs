using GymNexus.Core.Models;
using GymNexus.Infrastructure.Data;

namespace GymNexus.Core.Contracts;

public interface IAdminService
{
    Task<IEnumerable<OrderDto>> GetAllOrdersAsync(ApplicationUser user);

    Task<int> GetAllOrdersCountAsync();

    Task<int> GetPendingOrdersCountAsync();

    Task<int> GetConfirmedOrdersCountAsync();

    Task<int> GetCompletedOrdersCountAsync();
}