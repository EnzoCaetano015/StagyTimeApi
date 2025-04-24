using StagyTimeApi.Models;

namespace StagyTimeApi.Services.Interfaces
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(string email, string senha, string firstName, string lastName);
        Task<User> LoginAsync(string email, string senha);
    }
}
