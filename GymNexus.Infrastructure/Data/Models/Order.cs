using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymNexus.Infrastructure.Data.Models;

/// <summary>
/// Order entity representation in the system. Represents the orders that are made by the users. An order can have many order details.
/// </summary>
[Comment("Order entity representation in the system")]
public class Order
{
    /// <summary>
    /// The unique identifier of the order. Primary Key in the database.
    /// </summary>
    [Key]
    [Comment("The unique identifier of the order")]
    public int Id { get; set; }

    /// <summary>
    /// The unique identifier of the store from which the order was made. It is required. It is a foreign key to the Store entity.
    /// </summary>
    [Required]
    [Comment("The unique identifier of the store from which the order was made")]
    public int StoreId { get; set; }

    /// <summary>
    /// The store entity representation (Navigation property) from which the order was made. It is required.
    /// </summary>
    [ForeignKey(nameof(StoreId))]
    public Store Store { get; set; } = null!;

    /// <summary>
    /// The unique identifier of the user who made the order. It is required. It is a foreign key to the ApplicationUser entity.
    /// </summary>
    [Required]
    [Comment("The unique identifier of the user who made the order")]
    public string CreatedBy { get; set; } = null!;

    /// <summary>
    /// The user entity representation (Navigation property) who made the order. It is required.
    /// </summary>
    [ForeignKey(nameof(CreatedBy))]
    public ApplicationUser Creator { get; set; } = null!;

    /// <summary>
    /// The date and time when the order was made. Set on creation of the order. It is required.
    /// </summary>
    [Required]
    [Comment("The date and time when the order was made. Set on creation of the order")]
    public DateTime CreatedOn { get; set; }

    /// <summary>
    /// The price for a unit in the order. It is required. It is a decimal number with 4 digits in total, and 2 of them are after the decimal point.
    /// </summary>
    [Required]
    [Comment("The price for a unit in the order")]
    [Column(TypeName = "decimal(4,2)")]
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// The quantity of the products in the order. It is required. Based on this quantity and the unit price, the total price is calculated.
    /// </summary>
    [Required]
    [Comment("The quantity of the products in the order")]
    public int Quantity { get; set; }

    /// <summary>
    /// The total price of the order. It is required. It is a decimal number with 9 digits in total, and 2 of them are after the decimal point.
    /// </summary>
    [Required]
    [Comment("The total price of the order")]
    [Column(TypeName = "decimal(9,2)")]
    public decimal TotalPrice { get; set; }

    /// <summary>
    /// The status of the order. It is required. It represents the current status of the order. It can be "In Progress" or "Completed".
    /// </summary>
    [Required]
    [Comment("The status of the order")]
    public string Status { get; set; } = null!;

    /// <summary>
    /// The payment method for the order. It is required. It represents the method that the user used to pay for the order. It can be "In cash", "By Card" or "Paypal"
    /// </summary>
    [Required]
    [Comment("The payment method for the order")]
    public string PaymentMethod { get; set; } = null!;

    /// <summary>
    /// The status of the order. Represents if it is active or not. Set to true by default.
    /// </summary>
    [DefaultValue(true)]
    [Comment("The status of the order. Represents if it is active or not. Set to true by default")]
    public bool IsActive { get; set; }

    /// <summary>
    /// The collection of all order details that the order has. It is a navigation property. Gets the other products that could be in this order
    /// </summary>
    public virtual IEnumerable<OrderDetail> OrdersDetails { get; set; } = new List<OrderDetail>();
}