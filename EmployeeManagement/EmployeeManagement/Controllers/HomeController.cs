﻿using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers;

public class HomeController : Controller
{
    private readonly IEmployeeRepository _employeeRepository;

    public HomeController(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public ViewResult Index()
    {
        var model = _employeeRepository.getAllEmployees();
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
    public IActionResult Create(Employee employee)
    {
        if (ModelState.IsValid)
        {
            var newEmployee = _employeeRepository.Add(employee);
            return RedirectToAction("Details", new { id = newEmployee.Id });
        }

        return View();
    }
}