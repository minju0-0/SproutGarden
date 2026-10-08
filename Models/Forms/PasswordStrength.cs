using System.Text.RegularExpressions;

namespace BlazorSprout.Models;

/// <summary>Result of the live checklist on the Sign Up screen (5 rules, 20 % each).</summary>
public sealed record PasswordStrength(
    bool HasMinLength,
    bool HasUpper,
    bool HasLower,
    bool HasNumber,
    bool HasSpecial)
{
    private static readonly Regex Special = new(@"[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?~]", RegexOptions.Compiled);

    public int Score =>
        (HasMinLength ? 1 : 0) + (HasUpper ? 1 : 0) + (HasLower ? 1 : 0) +
        (HasNumber ? 1 : 0) + (HasSpecial ? 1 : 0);

    public string Label => Score switch
    {
        1 or 2 => "Weak",
        3 or 4 => "Good",
        5 => "Strong",
        _ => ""
    };

    public static PasswordStrength Evaluate(string? password)
    {
        password ??= string.Empty;
        return new PasswordStrength(
            HasMinLength: password.Length >= 8,
            HasUpper: password.Any(char.IsUpper),
            HasLower: password.Any(char.IsLower),
            HasNumber: password.Any(char.IsDigit),
            HasSpecial: Special.IsMatch(password));
    }
}
