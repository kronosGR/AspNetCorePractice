﻿using System;
using System.IO;
using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IWebHostEnvironment _hostingEnvironment;
    private readonly ILogger _logger;

    public HomeController(
        IEmployeeRepository employeeRepository,
        IWebHostEnvironment IHostingEnvironment,
        ILogger<HomeController> logger
    )
    {
        _employeeRepository = employeeRepository;
        _hostingEnvironment = IHostingEnvironment;
        _logger = logger;
    }

    [AllowAnonymous]
    public ViewResult Index()
    {
        var model = _employeeRepository.getAllEmployee();
        // if different name then have to specify the location
        // return View("~/Views/Home/index.cshtml",model);
        return View(model);
    }

    [AllowAnonymous]
    public ViewResult Details(int? id)
    {
        var employee = _employeeRepository.GetEmployee(id.Value);
        if (employee == null)
        {
            Response.StatusCode = 404;
            return View("EmployeeNotFound", id.Value);
        }

        var homeDetailsViewModel = new HomeDetailsViewModel
        {
            Employee = employee,
            PageTitle = "Employee Details"
        };
        return View(homeDetailsViewModel);
    }

    [HttpGet]
    // [Authorize]
    public ViewResult Create()
    {
        return View();
    }

    [HttpGet]
    // [Authorize]
    public ViewResult Edit(int id)
    {
        var employee = _employeeRepository.GetEmployee(id);
        var employeeEditViewModel = new EmployeeEditViewModel
        {
            Id = employee.Id,
            Name = employee.Name,
            Email = employee.Email,
            Department = employee.Department,
            ExistingPhotoPath = employee.PhotoPath
        };

        return View(employeeEditViewModel);
    }

    [HttpPost]
    // [Authorize]
    public IActionResult Create(EmployeeCreateViewModel model)
    {
        if (ModelState.IsValid)
        {
            var uniqueFileName = PreocessUploadedFile(model);
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

    [HttpPost]
    // [Authorize]
    public IActionResult Edit(EmployeeEditViewModel model)
    {
        if (ModelState.IsValid)
        {
            var employee = _employeeRepository.GetEmployee(model.Id);
            employee.Name = model.Name;
            employee.Email = model.Email;
            employee.Department = model.Department;
            if (model.Photos != null)
            {
                if (model.ExistingPhotoPath != null)
                {
                    var filePath = Path.Combine(
                        _hostingEnvironment.WebRootPath,
                        "images",
                        model.ExistingPhotoPath
                    );
                    System.IO.File.Delete(filePath);
                }

                employee.PhotoPath = PreocessUploadedFile(model);
            }

            var uniqueFileName = PreocessUploadedFile(model);

            _employeeRepository.Update(employee);
            return RedirectToAction("Index");
        }

        return View();
    }

    private string PreocessUploadedFile(EmployeeCreateViewModel model)
    {
        string uniqueFileName = null;
        if (model.Photos != null && model.Photos.Count > 0)
            foreach (var photo in model.Photos)
            {
                var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid() + "_" + photo.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var filestream = new FileStream(filePath, FileMode.Create))
                {
                    photo.CopyTo(filestream);
                }
            }

        return uniqueFileName;
    }
}