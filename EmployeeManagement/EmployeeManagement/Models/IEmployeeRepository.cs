﻿using System.Collections.Generic;

namespace EmployeeManagement.Models;

// it is important for Dependency injection
public interface IEmployeeRepository
{
    Employee GetEmployee(int Id);
    IEnumerable<Employee> getAllEmployee();
    Employee Add(Employee employee);
    Employee Update(Employee employeeChanges);
    Employee Delete(int Id);
}