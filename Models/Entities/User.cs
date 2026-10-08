using System.ComponentModel.DataAnnotations;

namespace BlazorSprout.Models;

/// <summary>An account created on the Sign Up screen.</summary>
public class User
{
    public int Id { get; set; }

    [Required, StringLength(40)]
    public string FirstName { get; set; } = string.Empty;

    [Required, StringLength(40)]
    public string LastName { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(254)]
    public string Email { get; set; } = string.Empty;

    /// <summary>Hash only - never store the plain password.</summary>
    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastSignInAt { get; set; }

    /// <summary>First + last name, for greetings and display.</summary>
    public string FullName => $"{FirstName} {LastName}".Trim();

    // Navigation
    public Garden? Garden { get; set; }
    public ICollection<FeedbackEntry> Feedback { get; set; } = new List<FeedbackEntry>();
}
