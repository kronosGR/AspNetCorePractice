namespace DishesAPI.EndpointFilters
{
    public class LogNotFoundResponseFilter : IEndpointFilter
    {
        private readonly ILogger<LogNotFoundResponseFilter> _logger;

        public LogNotFoundResponseFilter(ILogger<LogNotFoundResponseFilter> logger)
        {
            _logger = logger;
        }
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var result = await next(context);
            // ...

            return result;
        }
    }
}
