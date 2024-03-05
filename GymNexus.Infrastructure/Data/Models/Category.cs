using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static GymNexus.Infrastructure.Constants.DataConstants;

namespace GymNexus.Infrastructure.Data.Models;

/// <summary>
/// Represents a category in the system, which is used to specify what is the type of the products.
/// </summary>
[Comment("Category entity representation in the system")]
public class Category
{
    /// <summary>
    /// The unique identifier of the category. Primary Key in the database.
    /// </summary>
    [Key]
    [Comment("The unique identifier of the category")]
    public int Id { get; set; }

    /// <summary>
    /// The name of the category. It is required and has a maximum length of 50 characters.
    /// </summary>
    [Required]
    [MaxLength(CategoryNameMaxLength)]
    [Comment("The name of the category")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// The description of the category. It is required and has a maximum length of 500 characters.
    /// </summary>
    [Required]
    [MaxLength(CategoryDescriptionMaxLength)]
    [Comment("The description of the category")]
    public string Description { get; set; } = null!;

    /// <summary>
    /// The status of the category. Represents if it is active or not.
    /// </summary>
    [Comment("The status of the category. If it is active or not")]
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// The products that are related to the current category. A category can have many products. It is a navigation property.
    /// </summary>
    public virtual IEnumerable<Product> Products { get; set; } = new List<Product>();
}