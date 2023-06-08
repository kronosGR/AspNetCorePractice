namespace FlightPlanAPI.Authentication
{
    public interface IUserService
    {
        public Task<User> Authenticate(string username, string password);
    }
}
