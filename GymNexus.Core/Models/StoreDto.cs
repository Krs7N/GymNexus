namespace GymNexus.Core.Models;

public class StoreDto
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string CreatedOn { get; set; } = string.Empty;

    public decimal AverageRating { get; set; }

    public int RatingsCount { get; set; }

    public string Owner { get; set; } = string.Empty;

    public MarketplaceViewDto? Marketplace { get; set; }
}