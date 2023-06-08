namespace FlightPlanAPI.Authentication
{
    public class UserService : IUserService
    {
        // example simple local...
        public Task<User> Authenticate(string username, string password)
        {
            if (username != "admin" || password != "admin")
            {
                return Task.FromResult<User>(null);
            }

            var user = new User
            {
                Username = username,
                Id = Guid.NewGuid().ToString("N")
            };

            return Task.FromResult<User>(user);
        }
    }
}
