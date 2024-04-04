namespace GymNexus.Core.Models;

public class OrderFormDto
{
    public ProductCartDto[] Products { get; set; }

    public string PaymentMethod { get; set; } = string.Empty;
}