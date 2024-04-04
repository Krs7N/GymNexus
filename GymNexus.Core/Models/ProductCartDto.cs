namespace GymNexus.Core.Models;

public class ProductCartDto
{
    public int Id { get; set; }

    public string ImageUrl { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int Quantity { get; set; }
}