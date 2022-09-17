using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Models;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>().HasData(new Employee
                { Id = 1, Name = "Mary", Department = Dept.HR, Email = "test@gmail.com" },
            new Employee { Id = 2, Name = "John", Department = Dept.IT, Email = "test1@gmail.com" }
        );
    }
}