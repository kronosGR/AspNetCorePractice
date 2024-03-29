﻿using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models;

public class Employee
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
    public string Name { get; set; }

    [Required]
    [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,})+)$", ErrorMessage = "Invalid email format")]
    [Display(Name = "Office Email")]
    public string Email { get; set; }

    [Required] public Dept? Department { get; set; }
    public string PhotoPath { get; set; }
}