using GymNexus.Core.Contracts;
using GymNexus.Core.Models;
using GymNexus.Core.Services;
using GymNexus.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GymNexus.Tests;

[TestFixture]
public class OrderServiceTests : TestBase
{
    private IOrderService _orderService;

    [SetUp]
    public void SetUp()
    {
        _orderService = new OrderService(_context);
    }

    [Test]
    public async Task CreateOrderAsyncCreatesOrder()
    {
        var orderFormDto = new OrderFormDto
        {
            PaymentMethod = "PayPal",
            Products = new ProductCartDto[]
            {
                new ProductCartDto
                {
                    Id = 1,
                    Name = "Product 1",
                    Price = 50m,
                    Quantity = 2,
                    ImageUrl = "https://www.example.com/image.jpg",
                }
            }
        };

        await _orderService.CreateOrderAsync(orderFormDto, User);

        var order = await _context.Orders.FirstOrDefaultAsync();

        Assert.NotNull(order);
        Assert.That(order.PaymentMethod, Is.EqualTo("PayPal"));
        Assert.That(order.TotalPrice, Is.EqualTo(100));
        Assert.That(order.Status, Is.EqualTo("Pending"));
    }

    [Test]
    public async Task CreateOrderAsyncThrowsIfProductNotFound()
    {
        var orderFormDto = new OrderFormDto
        {
            PaymentMethod = "PayPal",
            Products = new ProductCartDto[]
            {
                new ProductCartDto
                {
                    Id = 100,
                    Name = "Product 1",
                    Price = 50m,
                    Quantity = 2,
                    ImageUrl = "https://www.example.com/image.jpg",
                }
            }
        };

        Assert.ThrowsAsync<InvalidOperationException>(async () => await _orderService.CreateOrderAsync(orderFormDto, User));
    }

    [Test]
    public async Task CreateOrderAsyncThrowsIfProductIsNotActive()
    {
        Product.IsActive = false;

        var orderFormDto = new OrderFormDto
        {
            PaymentMethod = "PayPal",
            Products = new ProductCartDto[]
            {
                new ProductCartDto
                {
                    Id = 3,
                    Name = "Product 1",
                    Price = 50m,
                    Quantity = 2,
                    ImageUrl = "https://www.example.com/image.jpg",
                }
            }
        };

        Assert.ThrowsAsync<InvalidOperationException>(async () => await _orderService.CreateOrderAsync(orderFormDto, User));
    }
}