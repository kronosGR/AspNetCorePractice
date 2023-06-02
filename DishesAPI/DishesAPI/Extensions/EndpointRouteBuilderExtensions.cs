using DishesAPI.EndpointFilters;
using DishesAPI.EndpointHandlers;

namespace DishesAPI.Extensions
{
    public static class EndpointRouteBuilderExtensions
    {
        public static void RegisterDishesEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            var dishesEndpoints = endpointRouteBuilder.MapGroup("/dishes");
            var dishWithGuidIdEndpoints = dishesEndpoints.MapGroup("/{dishId:guid}");

            dishesEndpoints.MapGet("", DishesHandlers.GetDishesAsync);
            dishWithGuidIdEndpoints.MapGet("", DishesHandlers.GetDishByIdAsync)
                .WithName("GetDish");
            dishesEndpoints.MapGet("/{dishName}", DishesHandlers.GetDishByNameAsync);
            dishesEndpoints.MapPost("", DishesHandlers.CreateDishAsync).AddEndpointFilter<ValidateAnnotationsFilter>();
            dishWithGuidIdEndpoints.MapPut("", DishesHandlers.UpdateDishAsync).AddEndpointFilter(
                new DishIsLockedFilter(new("wfgrewg rgid")));
            dishWithGuidIdEndpoints.MapDelete("", DishesHandlers.DeleteDishAsync).AddEndpointFilter<LogNotFoundResponseFilter>();
        }

        public static void RegisterIngredientEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            var ingredientsEndpoints = endpointRouteBuilder.MapGroup("/dishes/{dishId:guid}/ingredients");

            ingredientsEndpoints.MapGet("", IngredientsHandlers.GetIngredientsAsync);
        }
    }
}
