using DishesAPI.EndpointFilters;
using DishesAPI.EndpointHandlers;
using DishesAPI.Models;

namespace DishesAPI.Extensions
{
    public static class EndpointRouteBuilderExtensions
    {
        public static void RegisterDishesEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            var dishesEndpoints = endpointRouteBuilder.MapGroup("/dishes").RequireAuthorization();
            var dishWithGuidIdEndpoints = dishesEndpoints.MapGroup("/{dishId:guid}");

            dishesEndpoints.MapGet("", DishesHandlers.GetDishesAsync);

            dishWithGuidIdEndpoints.MapGet("", DishesHandlers.GetDishByIdAsync).WithName("GetDish")
                .WithOpenApi(o =>
                {
                    // for testing 
                    o.Deprecated = true;
                    return o;
                })
                .WithSummary("Get a dish by providing an Id")
                .WithDescription("Get a dish by providing an Id longer description");

            dishesEndpoints.MapGet("/{dishName}", DishesHandlers.GetDishByNameAsync).AllowAnonymous();
            dishesEndpoints.MapPost("", DishesHandlers.CreateDishAsync).AddEndpointFilter<ValidateAnnotationsFilter>()
                .ProducesValidationProblem(400)
                .Accepts<DishForCreationDto>("application/json");
            dishWithGuidIdEndpoints.MapPut("", DishesHandlers.UpdateDishAsync).AddEndpointFilter(
                new DishIsLockedFilter(new("wfgrewg rgid")));
            dishWithGuidIdEndpoints.MapDelete("", DishesHandlers.DeleteDishAsync).AddEndpointFilter<LogNotFoundResponseFilter>();
        }

        public static void RegisterIngredientEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            var ingredientsEndpoints = endpointRouteBuilder.MapGroup("/dishes/{dishId:guid}/ingredients")
                .RequireAuthorization();

            ingredientsEndpoints.MapGet("", IngredientsHandlers.GetIngredientsAsync);
        }
    }
}
