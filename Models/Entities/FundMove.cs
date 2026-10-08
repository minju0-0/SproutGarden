using System.ComponentModel.DataAnnotations;

namespace BlazorSprout.Models;

/// <summary>
/// History of the Budget screen's "move money" action between envelopes.
/// A null side means the Unallocated pool.
/// </summary>
public class FundMove : IValidatableObject
{
    public int Id { get; set; }
    public int GardenId { get; set; }

    /// <summary>Source category, or null = Unallocated.</summary>
    public int? FromCategoryId { get; set; }

    /// <summary>Destination category, or null = Unallocated.</summary>
    public int? ToCategoryId { get; set; }

    [Range(typeof(decimal), "0.01", "1000000000", ErrorMessage = "Amount must be greater than zero.")]
    public decimal Amount { get; set; }

    public DateTime MovedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public Garden Garden { get; set; } = null!;
    public BudgetCategory? FromCategory { get; set; }
    public BudgetCategory? ToCategory { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (FromCategoryId == ToCategoryId)
            yield return new ValidationResult("Pick two different envelopes.", new[] { nameof(ToCategoryId) });
    }
}
