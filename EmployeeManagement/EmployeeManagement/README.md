# Asp.Net core Practice

* Asp.net core
* Entity Framework Core
* MySql

## Notes

### For EF core

*Install dotnet ef tools*

```
    dotnet tool install --global-ef
 ```

*Create first/any migration*

```
    dotnet ef migrations add InitialMigration
```

*Update the database after evert migration creation*

```
    dotnet ef database update
```

### For mysql provider

*remove previous mysql provider*

```
    dotnet remove package MySql.EntityFrameworkCore
```

*add pomelo provider*

```
    dotnet add package Pomelo.EntityFrameworkCore.MySql
```

*sample configuration*

```
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        // Replace with your connection string.
        var connectionString = "server=localhost;user=root;password=1234;database=ef";

        // Replace with your server version and type.
        // Use 'MariaDbServerVersion' for MariaDB.
        // Alternatively, use 'ServerVersion.AutoDetect(connectionString)'.
        // For common usages, see pull request #1233.
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));

        // Replace 'YourDbContext' with the name of your own DbContext derived class.
        services.AddDbContext<YourDbContext>(
            dbContextOptions => dbContextOptions
                .UseMySql(connectionString, serverVersion)
                // The following three options help with debugging, but should
                // be changed or removed for production.
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
        );
    }
}
```
