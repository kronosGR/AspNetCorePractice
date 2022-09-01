using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;

        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>()
            {
                new Employee() { Id = 1, Name = "Mary", Department = "HR", Email = "test@gmail.com" },
                new Employee() { Id = 2, Name = "John", Department = "IT", Email = "test1@gmail.com" },
                new Employee() { Id = 3, Name = "Sam", Department = "IT", Email = "test2@gmail.com" },
            };
        }
        public Employee GetEmployee(int Id)
        {
            return _employeeList.FirstOrDefault(e => e.Id == Id);
        }
    }
}
