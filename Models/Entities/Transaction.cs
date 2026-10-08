using System.ComponentModel.DataAnnotations;

namespace BlazorSprout.Models;

/// <summary>
/// One expense. Step 2 of "How it works": it "waters or drains" its plant the instant it's saved.
/// </summary>
public class Transaction
{
    public int Id { get; set; }
    public int GardenId { get; set; }
    public int CategoryId { get; set; }

    /// <summary>Always positive; every transaction is a spend.</summary>
    [Range(typeof(decimal), "0.01", "1000000000", ErrorMessage = "Amount must be greater than zero.")]
    public decimal Amount { get; set; }

    public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Today);

    [StringLength(200)]
    public string? Note { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public Garden Garden { get; set; } = null!;
    public BudgetCategory Category { get; set; } = null!;
}
