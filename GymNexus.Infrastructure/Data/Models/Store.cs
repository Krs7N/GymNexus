using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static GymNexus.Infrastructure.Constants.DataConstants;

namespace GymNexus.Infrastructure.Data.Models;

/// <summary>
/// Represents a store in the system, which is owned by a user and can have a marketplace.
/// </summary>
[Comment("Store entity representation in the system")]
public class Store
{
    /// <summary>
    /// The unique identifier of the store entity.
    /// </summary>
    [Key]
    [Comment("The unique identifier of the store")]
    public int Id { get; set; }

    /// <summary>
    /// The name of the store. It is required and has a maximum length of 40 characters.
    /// </summary>
    [Required]
    [MaxLength(StoreNameMaxLength)]
    [Comment("The name of the store")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// The description of the store. It is required and has a maximum length of 500 characters.
    /// </summary>
    [Required]
    [MaxLength(StoreDescriptionMaxLength)]
    [Comment("The description of the store")]
    public string Description { get; set; } = null!;

    /// <summary>
    /// The date and time when the store was added to the system, and created. It is required.
    /// </summary>
    [Required]
    [Comment("The date and time when the store was added to the system, and created")]
    public DateTime CreatedOn { get; set; }

    /// <summary>
    /// The average rating that the store has received up to this moment. Defaults to 0.00 if the store has not been given rating yet.
    /// </summary>
    [DefaultValue(typeof(decimal), "0.00")]
    [Comment("The average rating that the store has received up to this moment")]
    [Column(TypeName = "decimal(2,2)")]
    public decimal AverageRating { get; set; }

    /// <summary>
    /// The count of all ratings that the store has received up to this moment. Defaults to 0 if the store has no ratings yet.
    /// </summary>
    [DefaultValue(0)]
    [Comment("The count of all ratings that the store has received up to this moment")]
    public int RatingsCount { get; set; }

    /// <summary>
    /// The status of the store. If it is active or not. Defaults to true.
    /// </summary>
    [Comment("The status of the store. If it is active or not")]
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// The unique identifier of the store's owner. It is required.
    /// </summary>
    [Required]
    [Comment("The unique identifier of the store's owner")]
    public string OwnerId { get; set; } = null!;

    /// <summary>
    /// The owner entity representation of the store (Navigation property). It is required.
    /// </summary>
    [ForeignKey(nameof(OwnerId))]
    public ApplicationUser Owner { get; set; } = null!;

    /// <summary>
    /// The unique identifier of the store's marketplace. The store initially starts without a marketplace and can continue be without one.
    /// </summary>
    [Comment("The unique identifier of the store's marketplace. The store initially starts without a marketplace and can continue be without one")]
    public int? MarketplaceId { get; set; }

    /// <summary>
    /// The marketplace entity representation of the store (Navigation property).
    /// </summary>
    [ForeignKey(nameof(MarketplaceId))]
    public Marketplace? Marketplace { get; set; }

    /// <summary>
    /// The collection of all products that the store is selling (Navigation property).
    /// </summary>
    public virtual IEnumerable<Product> Products { get; set; } = new List<Product>();

    /// <summary>
    /// The collection of all orders that the store has made (Navigation property).
    /// </summary>
    public virtual IEnumerable<Order> Orders { get; set; } = new List<Order>();
}