using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.ViewModels;

public class EditRoleViewModel
{
    public EditRoleViewModel()
    {
        Users = new List<string>();
    }

    public string Id { get; set; }

    [Required(ErrorMessage = "Role Name is required")]
    public string Rolename { get; set; }

    public List<string> Users { get; set; }
}