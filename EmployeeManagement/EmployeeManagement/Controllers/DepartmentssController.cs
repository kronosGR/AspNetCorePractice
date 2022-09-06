using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class DepartmentssController : Controller
    {
        public string List()
        {
            return "List() of deps";
        }

        public string Details()
        {
            return "Details() of Deps";
        }
    }
}
