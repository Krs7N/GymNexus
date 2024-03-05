using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static GymNexus.Infrastructure.Constants.DataConstants;

namespace GymNexus.Infrastructure.Data.Models;

/// <summary>
/// Represents a post in the system, which is created by a user. It can be used to share information in the community.
/// </summary>
[Comment("Post entity representation in the system")]
public class Post
{
    /// <summary>
    /// The unique identifier of the post. Primary Key in the database.
    /// </summary>
    [Key]
    [Comment("The unique identifier of the post")]
    public int Id { get; set; }

    /// <summary>
    /// The title of the post. It is required and has a maximum length of 50 characters.
    /// </summary>
    [Required]
    [MaxLength(PostTitleMaxLength)]
    [Comment("The title of the post")]
    public string Title { get; set; } = null!;

    /// <summary>
    /// The content of the post. It is required and has a maximum length of 500 characters.
    /// </summary>
    [Required]
    [MaxLength(PostContentMaxLength)]
    [Comment("The content of the post")]
    public string Content { get; set; } = null!;

    /// <summary>
    /// The URL representation of the post's image, if there is one. Post could have no image inserted. It has a maximum length of 250 characters.
    /// </summary>
    [MaxLength(PostImageUrlMaxLength)]
    [Comment("The URL representation of the post's image. Post could have no image inserted")]
    public string? ImageUrl { get; set; }

    /// <summary>
    /// The status of the post. Represents if the post is active or not. Set to true by default when posted.
    /// </summary>
    [Comment("The status of the post. If it is active or not. Set to true by default")]
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// The date and time when the post was added to the system. It is set when the entity is created.
    /// </summary>
    [Required]
    [Comment("The date and time when the post was added to the system")]
    public DateTime CreatedOn { get; set; }

    /// <summary>
    /// The unique identifier of the creator of the post. It is set on creation of the post.
    /// </summary>
    [Required]
    [Comment("The unique identifier of the creator of the post")]
    public string CreatedBy { get; set; } = null!;

    /// <summary>
    /// The creator entity representation (Navigation property) of the post.
    /// </summary>
    [ForeignKey(nameof(CreatedBy))]
    public ApplicationUser Creator { get; set; } = null!;

    /// <summary>
    /// The comments that are added to this post (Navigation property).
    /// </summary>
    public virtual IEnumerable<Comment> Comments { get; set; } = new List<Comment>();

    /// <summary>
    /// The likes that have been given to this post (Navigation property).
    /// </summary>
    public virtual IEnumerable<PostLike> PostsLikes { get; set; } = new List<PostLike>();
}