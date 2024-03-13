namespace GymNexus.Core.Models;

public class CommentDto
{
    public int? Id { get; set; }

    public string Content { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public bool IsEdited { get; set; }
}