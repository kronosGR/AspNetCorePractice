using DishesAPI.DbContexts;
using DishesAPI.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DishesDbContext>(o => o.UseSqlite(
    builder.Configuration["ConnectionStrings:DishesDBConnectionString"]));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.

var app = builder.Build();


// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.RegisterDishesEndpoints();
app.RegisterIngredientEndpoints();


using (var serviceScope = app.Services.GetService<IServiceScopeFactory>()
           .CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<DishesDbContext>();
    context.Database.Migrate();
}

app.Run();

