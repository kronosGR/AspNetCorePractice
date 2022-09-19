using EmployeeManagement.ViewModels;

namespace EmployeeManagement.Models;

public class EmployeeEditViewModel : EmployeeCreateViewModel
{
    public int Id { get; set; }
    public string ExistingPhotoPath { get; set; }
}