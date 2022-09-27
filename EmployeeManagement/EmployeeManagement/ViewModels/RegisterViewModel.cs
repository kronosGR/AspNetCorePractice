using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.ViewModels;

public class RegisterViewModel
{
    [Required]
    [EmailAddress]
    [Remote("IsEmailInUse", "Account")]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "Password and confirmation password do not match")]
    public string ConfirmPassword { get; set; }
}