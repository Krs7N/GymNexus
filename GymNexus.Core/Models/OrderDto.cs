namespace GymNexus.Core.Models;

public class OrderDto
{
    public int Id { get; set; }

    public string CreatedBy { get; set; } = string.Empty;

    public string CreatedOn { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public decimal TotalPrice { get; set; }

    public string Status { get; set; } = string.Empty;

    public ProductOrderDto[] Products { get; set; }
}