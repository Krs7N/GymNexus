namespace GymNexus.Core.Models;

public class ProductDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public string ImageUrl { get; set; } = string.Empty;

    public string CreatedOn { get; set; } = string.Empty;

    public StoreDto Store { get; set; }

    public CategoryDto Category { get; set; }

    public string? Marketplace { get; set; }

    public int Likes { get; set; }

    public bool IsLikedByCurrentUser { get; set; }
}