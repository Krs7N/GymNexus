namespace GymNexus.Core.Models;

public class MarketplaceDto
{
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Address { get; set; } = null!;

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }
}