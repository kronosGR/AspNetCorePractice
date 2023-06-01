using AutoMapper;
using DishesAPI.DbContexts;
using DishesAPI.Entities;
using DishesAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DishesDbContext>(o => o.UseSqlite(
    builder.Configuration["ConnectionStrings:DishesDBConnectionString"]));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.

var app = builder.Build();


// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

var dishesEndpoints = app.MapGroup("/dishes");
var dishWithGuidEndpoints = dishesEndpoints.MapGroup("/{dishId:guid}");
var ingredientsEndpoints = dishWithGuidEndpoints.MapGroup("/ingredients");


dishesEndpoints.MapGet("", async Task<Ok<IEnumerable<DishDto>>> (DishesDbContext dishesDbContext, IMapper mapper,
    ClaimsPrincipal claimsPrincipal, [FromQueryAttribute] string? name) =>
{
    Console.WriteLine($"User authenticated? {claimsPrincipal.Identity?.IsAuthenticated}");

    return TypedResults.Ok(mapper.Map<IEnumerable<DishDto>>(await dishesDbContext.Dishes
        .Where(d => name == null || d.Name.Contains(name))
        .ToListAsync()));
});

dishWithGuidEndpoints.MapGet("", async Task<Results<NotFound, Ok<DishDto>>> (DishesDbContext dishesDbContext,
    IMapper mapper, Guid dishId) =>
{
    var dishEntity = await dishesDbContext.Dishes.FirstOrDefaultAsync(d => d.Id == dishId);
    if (dishEntity == null)
    {
        return TypedResults.NotFound();
    }

    return TypedResults.Ok(mapper.Map<DishDto>(dishEntity));

}).WithName("GetDish");

ingredientsEndpoints.MapGet("", async (DishesDbContext disheDbContext, IMapper mapper, Guid dishId) =>
{
    return mapper.Map<IEnumerable<IngredientDto>>((await disheDbContext.Dishes.Include(d => d.Ingredients)
        .FirstOrDefaultAsync(d => d.Id == dishId))?.Ingredients);
});

dishesEndpoints.MapGet("/{dishName}", async (DishesDbContext dishesDbContext, IMapper mapper, string dishName) =>
{
    return mapper.Map<DishDto>(await dishesDbContext.Dishes.FirstOrDefaultAsync(d => d.Name == dishName));
});

dishesEndpoints.MapPost("", async (DishesDbContext dishesDbContext, IMapper mapper,
    [FromBody] DishForCreationDto dishForCreationDto
    //LinkGenerator linkGenerator,
    //HttpContext httpContext
    ) =>
{

    var dishEntity = mapper.Map<Dish>(dishForCreationDto);
    dishesDbContext.Add(dishEntity);
    await dishesDbContext.SaveChangesAsync();

    var dishToReturn = mapper.Map<DishDto>(dishEntity);

    //var linkToDish = linkGenerator.GetUriByName(httpContext, "GetDish", new { dishId = dishToReturn.Id });
    //return TypedResults.Created(linkToDish, dishToReturn);

    return TypedResults.CreatedAtRoute(dishToReturn, "GetDish", new { dishId = dishToReturn.Id });
});

dishWithGuidEndpoints.MapPut("", async Task<Results<NotFound, NoContent>> (DishesDbContext dishesDbContext, IMapper mapper, Guid dishId,
    DishForUpdateDto dishToUpdateDto) =>
{
    var dishEntity = await dishesDbContext.Dishes.FirstOrDefaultAsync(d => d.Id == dishId);
    if (dishEntity == null)
    {
        return TypedResults.NotFound();
    }

    mapper.Map(dishToUpdateDto, dishEntity);
    await dishesDbContext.SaveChangesAsync();

    return TypedResults.NoContent();

});

dishWithGuidEndpoints.MapDelete("", async Task<Results<NoContent, NotFound>> (DishesDbContext dishesDbContext, Guid dishId) =>
{
    var dishEntity = await dishesDbContext.Dishes.FirstOrDefaultAsync(d => d.Id == dishId);
    if (dishEntity == null)
    {
        return TypedResults.NotFound();
    }

    dishesDbContext.Dishes.Remove(dishEntity);
    await dishesDbContext.SaveChangesAsync();
    return TypedResults.NoContent();
});



using (var serviceScope = app.Services.GetService<IServiceScopeFactory>()
           .CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<DishesDbContext>();
    context.Database.Migrate();
}

app.Run();

