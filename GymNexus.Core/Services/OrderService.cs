using GymNexus.Core.Contracts;
using GymNexus.Core.Models;
using GymNexus.Core.Utils;
using GymNexus.Infrastructure.Data;
using GymNexus.Infrastructure.Data.Models;

namespace GymNexus.Core.Services;

public class OrderService : IOrderService
{
    private readonly ApplicationDbContext _context;

    public OrderService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateOrderAsync(OrderFormDto orderDto, string userId)
    {
        var order = new Order
        {
            CreatedBy = userId,
            CreatedOn = DateTime.Now,
            Quantity = orderDto.Products.Sum(p => p.Quantity),
            TotalPrice = orderDto.Products.Sum(p => p.Quantity * p.Price),
            PaymentMethod = orderDto.PaymentMethod,
            Status = OrderStatus.Pending.ToString()
        };

        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();

        foreach (var orderProduct in orderDto.Products)
        {
            var product = await _context.Products.FindAsync(orderProduct.Id);

            if (product == null || !product.IsActive)
            {
                throw new InvalidOperationException();
            }

            var orderEntity = await _context.Orders.FindAsync(order.Id);

            var orderDetail = new OrderDetail
            {
                OrderId = orderEntity!.Id,
                ProductId = product.Id,
                Quantity = orderProduct.Quantity
            };

            await _context.OrdersDetails.AddAsync(orderDetail);
        }

        await _context.SaveChangesAsync();
    }
}