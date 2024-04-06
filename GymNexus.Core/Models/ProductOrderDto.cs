namespace GymNexus.Core.Models;

public class ProductOrderDto
{
    public string Name { get; set; } = string.Empty;

    public string ImageUrl { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public decimal Price { get; set; }
}