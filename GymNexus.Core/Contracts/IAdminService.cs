using GymNexus.Core.Models;
using GymNexus.Infrastructure.Data;

namespace GymNexus.Core.Contracts;

public interface IAdminService
{
    Task<IEnumerable<OrderDto>> GetAllOrdersAsync();

    Task<int> GetAllOrdersCountAsync();

    Task<int> GetPendingOrdersCountAsync();

    Task<int> GetConfirmedOrdersCountAsync();

    Task<int> GetCompletedOrdersCountAsync();

    Task<string> ChangeOrderStatusAsync(int id, string status);
}