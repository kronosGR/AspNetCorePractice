using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement.Models;

public class MockEmployeeRepository : IEmployeeRepository
{
    private readonly List<Employee> _employeeList;

    public MockEmployeeRepository()
    {
        _employeeList = new List<Employee>
        {
            new() { Id = 1, Name = "Mary", Department = Dept.HR, Email = "test@gmail.com" },
            new() { Id = 2, Name = "John", Department = Dept.IT, Email = "test1@gmail.com" },
            new() { Id = 3, Name = "Sam", Department = Dept.IT, Email = "test2@gmail.com" }
        };
    }

    public Employee GetEmployee(int Id)
    {
        return _employeeList.FirstOrDefault(e => e.Id == Id);
    }

    public IEnumerable<Employee> getAllEmployee()
    {
        return _employeeList;
    }

    public Employee Add(Employee employee)
    {
        employee.Id = _employeeList.Max(e => e.Id) + 1;
        _employeeList.Add(employee);
        return employee;
    }
}