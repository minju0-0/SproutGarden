using System.ComponentModel.DataAnnotations;

namespace BlazorSprout.Models;

/// <summary>
/// A review from the Feedback page. Named FeedbackEntry (not Feedback) so it
/// doesn't clash with the Feedback.razor page component.
/// </summary>
public class FeedbackEntry
{
    public int Id { get; set; }

    /// <summary>Null for anonymous / not-yet-signed-in visitors.</summary>
    public int? UserId { get; set; }

    [Range(1, 5, ErrorMessage = "Please select a rating before planting.")]
    public int Rating { get; set; }

    [StringLength(1000, ErrorMessage = "Comment is too long.")]
    public string Comment { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // Navigation
    public User? User { get; set; }
}
