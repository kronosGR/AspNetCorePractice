using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Models;

public class SQLEmployeeRepository : IEmployeeRepository
{
    private readonly ILogger<SQLEmployeeRepository> _logger;
    private readonly AppDbContext context;

    public SQLEmployeeRepository(AppDbContext context, ILogger<SQLEmployeeRepository> _logger)
    {
        this.context = context;
        this._logger = _logger;
    }

    public Employee GetEmployee(int Id)
    {
        return context.Employees.Find(Id);
    }

    public IEnumerable<Employee> getAllEmployee()
    {
        return context.Employees;
    }

    public Employee Add(Employee employee)
    {
        context.Employees.Add(employee);
        context.SaveChanges();
        return employee;
    }

    public Employee Update(Employee employeeChanges)
    {
        var employee = context.Employees.Attach(employeeChanges);
        employee.State = EntityState.Modified;
        context.SaveChanges();
        return employeeChanges;
    }

    public Employee Delete(int Id)
    {
        var employee = context.Employees.Find(Id);
        if (employee != null) context.Employees.Remove(employee);

        context.SaveChanges();
        return employee;
    }
}