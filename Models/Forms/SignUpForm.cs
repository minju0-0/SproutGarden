using System.ComponentModel.DataAnnotations;

namespace BlazorSprout.Models;

/// <summary>Bound to the form on SignUp.razor.</summary>
public class SignUpForm
{
    [Required(ErrorMessage = "First name is required.")]
    [StringLength(40, ErrorMessage = "First name is too long.")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required.")]
    [StringLength(40, ErrorMessage = "Last name is too long.")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email address is required.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters.")]
    public string Password { get; set; } = string.Empty;

    public string FullName => $"{FirstName} {LastName}".Trim();
}
