namespace Web.BFF.Interfaces;
public interface IGreetingService
{
    Task<string> GetGreetingByIdAsync(string id);

    Task SaveGreeting(string greeting);
}
