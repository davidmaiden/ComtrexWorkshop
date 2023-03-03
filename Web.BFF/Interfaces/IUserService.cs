namespace Web.BFF.Interfaces
{
    public interface IUserService
    {
        Task<object> GetUserAsync(string userId);
    }
}
