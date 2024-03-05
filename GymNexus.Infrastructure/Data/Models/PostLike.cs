using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymNexus.Infrastructure.Data.Models;

/// <summary>
/// Post like entity representation in the system. Represents the amount of likes the post has received
/// </summary>
[Comment("Post like entity representation in the system. Represents the amount of likes the post has received")]
public class PostLike
{
    /// <summary>
    /// The unique identifier of the post like. Part of composite key in the database.
    /// </summary>
    [Required]
    [Comment("The unique identifier of the post")]
    public int PostId { get; set; }

    /// <summary>
    /// The post entity representation (Navigation property) to which the post like is related. It is required. It is a foreign key to the Post entity.
    /// </summary>
    [ForeignKey(nameof(PostId))]
    public Post Post { get; set; } = null!;

    /// <summary>
    /// The unique identifier of the user who liked the post. Part of composite key in the database.
    /// </summary>
    [Required]
    [Comment("The unique identifier of the user who liked the post")]
    public string UserId { get; set; } = null!;

    /// <summary>
    /// The user entity representation (Navigation property) who liked the post. It is required. It is a foreign key to the ApplicationUser entity.
    /// </summary>
    [ForeignKey(nameof(UserId))]
    public ApplicationUser User { get; set; } = null!;
}