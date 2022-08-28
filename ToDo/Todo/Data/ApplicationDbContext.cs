using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Todo.Models;

namespace Todo.Data;

public class ApplicationDbContext : IdentityDbContext
{
    // represents a table/collection in the dB
    public DbSet<ToDoItem> Items { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}
