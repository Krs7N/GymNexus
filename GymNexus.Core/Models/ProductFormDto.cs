namespace GymNexus.Core.Models;

public class ProductFormDto
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public string ImageUrl { get; set; } = string.Empty;

    public int StoreId { get; set; }

    public int CategoryId { get; set; }

    public int? MarketplaceId { get; set; }
}