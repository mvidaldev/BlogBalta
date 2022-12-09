using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModel;

public class LoginViewModel
{
    [EmailAddress(ErrorMessage = "You should type a valid email.")]
    public required string Email { get; set; }
    [Required(ErrorMessage = "Insert your Password ")]
    public required string Password { get; set; }
}