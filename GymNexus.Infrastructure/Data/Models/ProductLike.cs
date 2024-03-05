using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GymNexus.Infrastructure.Data.Models;

/// <summary>
/// Product like entity representation in the system. Represents the amount of likes the product has received
/// </summary>
[Comment("Product like entity representation in the system. Represents the amount of likes the product has received")]
public class ProductLike
{
    /// <summary>
    /// The unique identifier of the product like. Part of composite key in the database.
    /// </summary>
    [Required]
    [Comment("The unique identifier of the product")]
    public int ProductId { get; set; }

    /// <summary>
    /// The product entity representation (Navigation property) to which the product like is related. It is required. It is a foreign key to the Product entity.
    /// </summary>
    [ForeignKey(nameof(ProductId))]
    public Product Product { get; set; } = null!;

    /// <summary>
    /// The unique identifier of the user who liked the product. Part of composite key in the database.
    /// </summary>
    [Required]
    [Comment("The unique identifier of the user who liked the product")]
    public string UserId { get; set; } = null!;

    /// <summary>
    /// The user entity representation (Navigation property) who liked the product. It is required. It is a foreign key to the ApplicationUser entity.
    /// </summary>
    [ForeignKey(nameof(UserId))]
    public ApplicationUser User { get; set; } = null!;
}