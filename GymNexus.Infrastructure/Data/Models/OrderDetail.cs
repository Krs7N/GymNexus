using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymNexus.Infrastructure.Data.Models;

/// <summary>
/// The order details of an order with the different products entity representation in the system. An order detail is a product that is included in an order.
/// </summary>
[Comment("The order details of an order with the different products entity representation in the system")]
public class OrderDetail
{
    /// <summary>
    /// The unique identifier of the order detail. Part of composite key in the database.
    /// </summary>
    [Required]
    [Comment("The unique identifier of the order")]
    public int OrderId { get; set; }

    /// <summary>
    /// The order entity representation (Navigation property) to which the order detail is related. It is required. It is a foreign key to the Order entity.
    /// </summary>
    [ForeignKey(nameof(OrderId))]
    public Order Order { get; set; } = null!;

    /// <summary>
    /// The unique identifier of the product that is included in the order. Part of composite key in the database.
    /// </summary>
    [Required]
    [Comment("The unique identifier of the product that is included in the order")]
    public int ProductId { get; set; }

    /// <summary>
    /// The product entity representation (Navigation property) that is included in the order. It is required. It is a foreign key to the Product entity.
    /// </summary>
    [ForeignKey(nameof(ProductId))]
    public Product Product { get; set; } = null!;
}