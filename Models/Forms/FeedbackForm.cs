using System.ComponentModel.DataAnnotations;

namespace BlazorSprout.Models;

/// <summary>
/// Bound to the form on Feedback.razor. Rating has no [Range] on purpose: the page
/// shows its own "select a rating" message when it is still 0.
/// </summary>
public class FeedbackForm
{
    public int Rating { get; set; }

    [StringLength(1000, ErrorMessage = "Comment is too long.")]
    public string Comment { get; set; } = string.Empty;

    public FeedbackEntry ToEntry(int? userId = null) => new()
    {
        UserId = userId,
        Rating = Rating,
        Comment = Comment?.Trim() ?? string.Empty,
        CreatedAt = DateTime.Now
    };
}
