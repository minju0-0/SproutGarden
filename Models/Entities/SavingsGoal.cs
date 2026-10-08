using System.ComponentModel.DataAnnotations;

namespace BlazorSprout.Models;

/// <summary>A savings goal shown as a tree (e.g. "Japan Trip" $2,500).</summary>
public class SavingsGoal
{
    public int Id { get; set; }
    public int GardenId { get; set; }

    [Required(ErrorMessage = "Name your goal."), StringLength(80)]
    public string Name { get; set; } = string.Empty;

    [Range(typeof(decimal), "0.01", "1000000000", ErrorMessage = "Target must be greater than zero.")]
    public decimal TargetAmount { get; set; }

    public DateOnly? TargetDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>Set the first time the goal is fully funded (triggers the sparkle).</summary>
    public DateTime? AchievedAt { get; set; }

    // Navigation
    public Garden Garden { get; set; } = null!;
    public ICollection<GoalContribution> Contributions { get; set; } = new List<GoalContribution>();

    // Calculated (load Contributions first)
    public decimal SavedAmount => Contributions.Sum(c => c.Amount);

    /// <summary>0..1, clamped. Multiply by 100 for the progress bar.</summary>
    public decimal Progress => TargetAmount <= 0 ? 0 : Math.Min(1m, SavedAmount / TargetAmount);

    public bool IsAchieved => TargetAmount > 0 && SavedAmount >= TargetAmount;

    public TreeStage Stage => GardenRules.TreeStageFor(Progress);

    /// <summary>Adds money to the goal and stamps <see cref="AchievedAt"/> when the target is reached.</summary>
    public GoalContribution Contribute(decimal amount)
    {
        if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount), "Contribution must be positive.");

        var contribution = new GoalContribution { GoalId = Id, Goal = this, Amount = amount };
        Contributions.Add(contribution);

        if (AchievedAt is null && IsAchieved)
            AchievedAt = DateTime.UtcNow;

        return contribution;
    }
}
