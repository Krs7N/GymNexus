using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static GymNexus.Infrastructure.Constants.DataConstants;

namespace GymNexus.Infrastructure.Data.Models;

/// <summary>
/// Represents a product or products in the system, which are sold by a store.
/// </summary>
[Comment("Product entity representation in a user's store")]
public class Product
{
    /// <summary>
    /// The unique identifier of the product.
    /// </summary>
    [Key]
    [Comment("The unique identifier of the product")]
    public int Id { get; set; }

    /// <summary>
    /// The name of the product that is being sold.
    /// </summary>
    [Required]
    [MaxLength(ProductNameMaxLength)]
    [Comment("The name of the product")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// The description of the product.
    /// </summary>
    [Required]
    [MaxLength(ProductDescriptionMaxLength)]
    [Comment("The description of the product")]
    public string Description { get; set; } = null!;

    /// <summary>
    /// The URL representation of the product's image.
    /// </summary>
    [Required]
    [MaxLength(ProductImageUrlMaxLength)]
    [Comment("The URL representation of the product's image")]
    public string ImageUrl { get; set; } = null!;

    /// <summary>
    /// The price of the product with range between 1 and 1000 EUR.
    /// </summary>
    [Required]
    [Range(typeof(decimal), ProductPriceMinValue, ProductPriceMaxValue)]
    [Comment("The price of the product")]
    [Column(TypeName = "decimal(4,2)")]
    public decimal Price { get; set; }

    /// <summary>
    /// The date and time when the product was added to the system.
    /// </summary>
    [Required]
    [Comment("The date and time when the product was added to the system")]
    public DateTime CreatedOn { get; set; }

    /// <summary>
    /// Determines whether the product is still active in the system or not. Usually refers to if it has been deleted
    /// </summary>
    [Comment("Determines whether the product is still active in the system or not")]
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// The unique identifier of the store that is selling the current product.
    /// </summary>
    [Required]
    [Comment("The unique identifier of the store that is selling the current product")]
    public int StoreId { get; set; }

    /// <summary>
    /// The store entity representation (Navigation property) that is selling the current product.
    /// </summary>
    [ForeignKey(nameof(StoreId))]
    public Store Store { get; set; } = null!;

    /// <summary>
    /// The unique identifier of the category that the product belongs to.
    /// </summary>
    [Required]
    [Comment("The unique identifier of the category that the product belongs to")]
    public int CategoryId { get; set; }

    /// <summary>
    /// The category entity representation (Navigation property) that the product belongs to.
    /// </summary>
    [ForeignKey(nameof(CategoryId))]
    public Category Category { get; set; } = null!;

    /// <summary>
    /// The collection of all likes that the product has received (Navigation property).
    /// </summary>
    public virtual IEnumerable<ProductLike> ProductsLikes { get; set; } = new List<ProductLike>();
}