using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers;

public class AccountController : Controller
{
    // GET
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }
}