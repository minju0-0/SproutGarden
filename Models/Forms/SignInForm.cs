using System.ComponentModel.DataAnnotations;

namespace BlazorSprout.Models;

/// <summary>Bound to the form on SignIn.razor.</summary>
public class SignInForm
{
    [Required(ErrorMessage = "Email address is required.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; set; } = string.Empty;
}
