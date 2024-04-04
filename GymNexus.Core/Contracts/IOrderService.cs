using GymNexus.Core.Models;
using GymNexus.Infrastructure.Data;

namespace GymNexus.Core.Contracts;

public interface IOrderService
{
    Task CreateOrderAsync(OrderFormDto orderDto, ApplicationUser user);
}