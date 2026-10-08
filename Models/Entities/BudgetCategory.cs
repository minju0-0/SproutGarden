using System.ComponentModel.DataAnnotations;

namespace BlazorSprout.Models;

/// <summary>A budget envelope, shown as a plant in the Garden: a name and a monthly number.</summary>
public class BudgetCategory
{
    public int Id { get; set; }
    public int GardenId { get; set; }

    [Required(ErrorMessage = "Give your plant a name."), StringLength(60)]
    public string Name { get; set; } = string.Empty;

    [StringLength(8)]
    public string Emoji { get; set; } = "🌱";

    [Range(0, 1_000_000_000, ErrorMessage = "Limit can't be negative.")]
    public decimal MonthlyLimit { get; set; }

    public bool IsArchived { get; set; }

    // Navigation
    public Garden Garden { get; set; } = null!;
    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    // Calculated (load Transactions first)
    public decimal SpentIn(DateOnly month) =>
        Transactions.Where(t => t.Date.Year == month.Year && t.Date.Month == month.Month).Sum(t => t.Amount);

    public decimal RemainingIn(DateOnly month) => MonthlyLimit - SpentIn(month);

    public decimal UsageRatioIn(DateOnly month)
    {
        var spent = SpentIn(month);
        if (MonthlyLimit <= 0) return spent > 0 ? 1m : 0m;
        return spent / MonthlyLimit;
    }

    public PlantHealth HealthIn(DateOnly month) => GardenRules.PlantHealthFor(UsageRatioIn(month));
}
