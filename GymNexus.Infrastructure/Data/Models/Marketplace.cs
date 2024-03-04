using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using static GymNexus.Infrastructure.Constants.DataConstants;

namespace GymNexus.Infrastructure.Data.Models;

/// <summary>
/// Represents a marketplace in the system, which is used to specify where a store is located. Marketplace can have many stores.
/// </summary>
[Comment("Marketplace entity representation in the system")]
public class Marketplace
{
    /// <summary>
    /// The unique identifier of the marketplace. Primary Key in the database.
    /// </summary>
    [Key]
    [Comment("The unique identifier of the marketplace")]
    public int Id { get; set; }

    /// <summary>
    /// The name of the marketplace. It is required and has a maximum length of 50 characters.
    /// </summary>
    [Required]
    [MaxLength(MarketplaceNameMaxLength)]
    [Comment("The name of the marketplace")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// The description of the marketplace. It is required and has a maximum length of 500 characters.
    /// </summary>
    [Required]
    [MaxLength(MarketplaceDescriptionMaxLength)]
    [Comment("The description of the marketplace")]
    public string Description { get; set; } = null!;

    /// <summary>
    /// The address of the marketplace. It is required and has a maximum length of 150 characters.
    /// </summary>
    [Required]
    [MaxLength(MarketplaceAddressMaxLength)]
    [Comment("The address of the marketplace")]
    public string Address { get; set; } = null!;

    /// <summary>
    /// The latitude coordinate representation of the marketplace. It is required and it's value will be determined based on the provided address.
    /// </summary>
    [Required]
    [Comment("The latitude coordinate representation of the marketplace")]
    [Column(TypeName = "decimal(12,9)")]
    public decimal Latitude { get; set; }

    /// <summary>
    /// The longitude coordinate representation of the marketplace. It is required and it's value will be determined based on the provided address.
    /// </summary>
    [Required]
    [Comment("The longitude coordinate representation of the marketplace")]
    [Column(TypeName = "decimal(12,9)")]
    public decimal Longitude { get; set; }

    /// <summary>
    /// The current status of the marketplace. Represents if it is active or not.
    /// </summary>
    [DefaultValue(true)]
    [Comment("The status of the marketplace. If it is active or not")]
    public bool IsActive { get; set; }
}