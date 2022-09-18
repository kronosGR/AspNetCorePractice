using System.ComponentModel.DataAnnotations;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Http;

namespace EmployeeManagement.ViewModels;

public class EmployeeCreateViewModel
{
    [Required]
    [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
    public string Name { get; set; }

    [Required]
    [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,})+)$", ErrorMessage = "Invalid email format")]
    [Display(Name = "Office Email")]
    public string Email { get; set; }

    [Required] public Dept? Department { get; set; }
    public IFormFile Photo { get; set; }
}