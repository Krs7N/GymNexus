using GymNexus.Core.Models;

namespace GymNexus.Core.Contracts;

public interface IOrderService
{
    Task CreateOrderAsync(OrderFormDto orderDto, string userId);
}