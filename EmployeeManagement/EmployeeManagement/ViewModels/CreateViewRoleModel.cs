using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.ViewModels;

public class CreateViewRoleModel
{
    [Required] public string RoleName { get; set; }
}