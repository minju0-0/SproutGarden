using System.ComponentModel.DataAnnotations;

namespace BlazorSprout.Models;

/// <summary>
/// The user's whole budget. One garden per user. Categories are the plants,
/// goals are the trees. Monthly figures are always calculated for a given month.
/// </summary>
public class Garden
{
    public int Id { get; set; }
    public int UserId { get; set; }

    [Required, StringLength(80)]
    public string Name { get; set; } = "My Garden";

    /// <summary>Money available to allocate each month.</summary>
    [Range(0, 1_000_000_000)]
    public decimal MonthlyIncome { get; set; }

    [StringLength(3, MinimumLength = 3)]
    public string CurrencyCode { get; set; } = "USD";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public User User { get; set; } = null!;
    public ICollection<BudgetCategory> Categories { get; set; } = new List<BudgetCategory>();
    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    public ICollection<FundMove> FundMoves { get; set; } = new List<FundMove>();
    public ICollection<SavingsGoal> Goals { get; set; } = new List<SavingsGoal>();

    // ---- Calculated (not stored). Load Categories + Transactions first. ----

    /// <summary>Sum of all active category limits ("Budgeted" stat).</summary>
    public decimal TotalBudgeted => Categories.Where(c => !c.IsArchived).Sum(c => c.MonthlyLimit);

    /// <summary>Income not yet given to a category ("Unallocated" on the Budget screen).</summary>
    public decimal Unallocated => MonthlyIncome - TotalBudgeted;

    public decimal TotalSpentIn(DateOnly month) =>
        Categories.Where(c => !c.IsArchived).Sum(c => c.SpentIn(month));

    /// <summary>"Remaining" stat on the hero card.</summary>
    public decimal RemainingIn(DateOnly month) => TotalBudgeted - TotalSpentIn(month);

    /// <summary>Spent / budgeted for the month. 0 when nothing is budgeted.</summary>
    public decimal UsageRatioIn(DateOnly month) =>
        TotalBudgeted <= 0 ? 0 : TotalSpentIn(month) / TotalBudgeted;

    /// <summary>"28% left" label.</summary>
    public decimal PercentRemainingIn(DateOnly month) =>
        TotalBudgeted <= 0 ? 0 : Math.Max(0, Math.Round(RemainingIn(month) / TotalBudgeted * 100, 0));

    public WeatherCondition WeatherIn(DateOnly month) =>
        GardenRules.WeatherFor(UsageRatioIn(month));
}
