namespace EmployeeManagement.Models
{
    // it is important for Dependency injection
    public interface IEmployeeRepository
    {

        Employee GetEmployee(int Id);
    }
}
