using System.ComponentModel.DataAnnotations;

namespace BlazorSprout.Models;

/// <summary>Money put toward a <see cref="SavingsGoal"/>; makes its tree grow.</summary>
public class GoalContribution
{
    public int Id { get; set; }
    public int GoalId { get; set; }

    [Range(typeof(decimal), "0.01", "1000000000", ErrorMessage = "Amount must be greater than zero.")]
    public decimal Amount { get; set; }

    public DateTime ContributedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public SavingsGoal Goal { get; set; } = null!;
}
