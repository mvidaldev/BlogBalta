using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModel;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Name is mandatory")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Email is Mandatory")]
    [EmailAddress(ErrorMessage = "Invalid email.")]
    public required string Email { get; set; }
    
}