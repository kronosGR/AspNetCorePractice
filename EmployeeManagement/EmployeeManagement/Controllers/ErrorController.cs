using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers;

public class ErrorController : Controller
{
    // GET
    [Route("Error/{statusCode}")]
    public IActionResult HttpStatusCodeHandler(int statusCode)
    {
        switch (statusCode)
        {
            case 404:
                ViewBag.ErrorMessage = "Sorry the resource could not be found";
                break;
        }

        return View("NotFound");
    }
}