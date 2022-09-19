using System;
using System.IO;
using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers;

public class HomeController : Controller
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IWebHostEnvironment _hostingEnvironment;

    public HomeController(
        IEmployeeRepository employeeRepository,
        IWebHostEnvironment IHostingEnvironment
    )
    {
        _employeeRepository = employeeRepository;
        _hostingEnvironment = IHostingEnvironment;
    }

    public ViewResult Index()
    {
        var model = _employeeRepository.getAllEmployee();
        // if different name then have to specify the location
        // return View("~/Views/Home/index.cshtml",model);
        return View(model);
    }

    public ViewResult Details(int? id)
    {
        var homeDetailsViewModel = new HomeDetailsViewModel
        {
            Employee = _employeeRepository.GetEmployee(id ?? 1),
            PageTitle = "Employee Details"
        };
        return View(homeDetailsViewModel);
    }

    [HttpGet]
    public ViewResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(EmployeeCreateViewModel model)
    {
        if (ModelState.IsValid)
        {
            string uniqueFileName = null;
            if (model.Photos != null && model.Photos.Count > 0)
                foreach (var photo in model.Photos)
                {
                    var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid() + "_" + photo.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }

            var newEmployee = new Employee
            {
                Name = model.Name,
                Email = model.Email,
                Department = model.Department,
                PhotoPath = uniqueFileName
            };
            _employeeRepository.Add(newEmployee);
            return RedirectToAction("Details", new { id = newEmployee.Id });
        }

        return View();
    }
}