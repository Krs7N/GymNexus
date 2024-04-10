using GymNexus.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static GymNexus.Infrastructure.Constants.DataConstants;

namespace GymNexus.Infrastructure.Data;

/// <summary>
/// Application user entity representation in the system. Extends the default IdentityUser
/// </summary>
[Comment("Application user entity representation in the system. Extends the default IdentityUser")]
public class ApplicationUser : IdentityUser
{
    /// <summary>
    /// The URL representation of the user's profile picture. It has a maximum length of 250 characters. User could have no profile picture inserted.
    /// </summary>
    [MaxLength(ProfilePictureMaxLength)]
    [Comment("The URL representation of the user's profile picture")]
    public string? ProfilePictureUrl { get; set; }

    /// <summary>
    /// The first name of the user. It is not required at first and has a maximum length of 12 characters.
    /// </summary>
    [MaxLength(FirstNameMaxLength)]
    [Comment("The first name of the user")]
    public string? FirstName { get; set; }

    /// <summary>
    /// The last name of the user. It is not required at first and has a maximum length of 15 characters.
    /// </summary>
    [MaxLength(LastNameMaxLength)]
    [Comment("The last name of the user")]
    public string? LastName { get; set; }

    /// <summary>
    /// The collection of posts that the current user has created. A user can have many posts. It is a navigation property.
    /// </summary>
    public virtual IEnumerable<Post> Posts { get; set; } = new List<Post>();

    /// <summary>
    /// The collection of comments that the current user has created. A user can add many comments to a post. It is a navigation property.
    /// </summary>
    public virtual IEnumerable<Comment> Comments { get; set; } = new List<Comment>();

    /// <summary>
    /// The collection of stores that the current user has created. A user can have many stores. It is a navigation property.
    /// </summary>
    public virtual IEnumerable<Store> Stores { get; set; } = new List<Store>();
}