namespace Web.BFF.Interfaces;
public interface IGreetingService
{
    Task<string> GetGreetingByIdAsync(string id);

    Task SaveGreetingAsync(int id, string greeting);
}
