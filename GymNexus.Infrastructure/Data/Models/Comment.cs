using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static GymNexus.Infrastructure.Constants.DataConstants;

namespace GymNexus.Infrastructure.Data.Models;

/// <summary>
/// Represents a comment entity in the system, which is used to specify what is the opinion of the user for a post.
/// </summary>
[Comment("Comment entity representation in the system")]
public class Comment
{
    /// <summary>
    /// The unique identifier of the comment. Primary Key in the database.
    /// </summary>
    [Key]
    [Comment("The unique identifier of the comment")]
    public int Id { get; set; }

    /// <summary>
    /// The content of the comment. It is required and has a maximum length of 250 characters.
    /// </summary>
    [Required]
    [MaxLength(CommentContentMaxLength)]
    [Comment("The content of the comment")]
    public string Content { get; set; } = null!;

    /// <summary>
    /// The status of the comment. Represents if it is active or not. Comments could be deleted. Set to true by default when posted.
    /// </summary>
    [DefaultValue(true)]
    [Comment("The status of the comment. Represents if it is active or not. Comments could be deleted")]
    public bool IsActive { get; set; }

    /// <summary>
    /// The unique identifier of the post that the comment is related to. It is required. It is a foreign key to the Post entity.
    /// </summary>
    [Required]
    [Comment("The unique identifier of the post that the comment is related to")]
    public int PostId { get; set; }

    /// <summary>
    /// The post entity representation (Navigation property) that the comment is related to. It is required.
    /// </summary>
    [ForeignKey(nameof(PostId))]
    public Post Post { get; set; } = null!;

    /// <summary>
    /// The date and time when the comment was added to the system. It is required. It is set when the entity is created.
    /// </summary>
    [Required]
    [Comment("The date and time when the comment was added to the system")]
    public DateTime CreatedOn { get; set; }

    /// <summary>
    /// The unique identifier of the creator of the comment. It is set on creation of the comment. It is required. It is a foreign key to the ApplicationUser entity.
    /// </summary>
    [Required]
    [Comment("The unique identifier of the creator of the comment. It is set on creation of the comment")]
    public string CreatedBy { get; set; } = null!;

    /// <summary>
    /// The user entity representation (Navigation property) that the comment is related to. It is required.
    /// </summary>
    [ForeignKey(nameof(CreatedBy))]
    public ApplicationUser Creator { get; set; } = null!;
}