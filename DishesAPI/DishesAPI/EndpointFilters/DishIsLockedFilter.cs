namespace DishesAPI.EndpointFilters
{
    public class DishIsLockedFilter : IEndpointFilter
    {
        private readonly Guid _lockedDishId;

        public DishIsLockedFilter(Guid lockedDishId)
        {
            _lockedDishId = lockedDishId;
        }
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            // here add the code to filter the request
            return await next.Invoke(context);
        }
    }
}
