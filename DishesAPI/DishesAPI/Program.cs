using AutoMapper;
using DishesAPI.DbContexts;
using DishesAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DishesDbContext>(o => o.UseSqlite(
    builder.Configuration["ConnectionStrings:DishesDBConnectionString"]));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.

var app = builder.Build();


// Configure the HTTP request pipeline.

app.UseHttpsRedirection();


app.MapGet("/dishes", async (DishesDbContext dishesDbContext, IMapper mapper) =>
{
    return mapper.Map<IEnumerable<DishDto>>(await dishesDbContext.Dishes.ToListAsync());
});

app.MapGet("/dishes/{dishId:guid}", async (DishesDbContext dishesDbContext, IMapper mapper, Guid dishId) =>
{

    return mapper.Map<DishDto>(await dishesDbContext.Dishes.FirstOrDefaultAsync(d => d.Id == dishId));
});

app.MapGet("/dishes/{dishId:guid}/ingredients", async (DishesDbContext disheDbContext, IMapper mapper, Guid dishId) =>
{
    return mapper.Map<IEnumerable<IngredientDto>>((await disheDbContext.Dishes.Include(d => d.Ingredients)
        .FirstOrDefaultAsync(d => d.Id == dishId))?.Ingredients);
});

app.MapGet("/dishes/{dishName}", async (DishesDbContext dishesDbContext, IMapper mapper, string dishName) =>
{
    return mapper.Map<DishDto>(await dishesDbContext.Dishes.FirstOrDefaultAsync(d => d.Name == dishName));
});



using (var serviceScope = app.Services.GetService<IServiceScopeFactory>()
           .CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<DishesDbContext>();
    context.Database.Migrate();
}

app.Run();

