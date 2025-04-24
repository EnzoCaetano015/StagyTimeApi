using System.Security.Cryptography;
using System.Text;
using StagyTimeApi.Models;
using StagyTimeApi.Repositories.Interfaces;
using StagyTimeApi.Services.Interfaces;

namespace StagyTimeApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        public AuthService(IUserRepository userRepo)=> _userRepo = userRepo;

        public async Task<User> RegisterAsync(string email, string senha, string firstName, string lastName)
        {
            var existing = await _userRepo.GetByEmailAsync(email);
            if (existing != null)
                throw new Exception("User already exists");

            using var sha = SHA256.Create();
            var has = sha.ComputeHash(Encoding.UTF8.GetBytes(senha));
            var senhaHash = Convert.ToBase64String(has);

            var user = new User
            {
                Email = email,
                Senha = senhaHash,
                FirstName = firstName,
                LastName = lastName,
                IdEmpresa = null,  
                IdTipoUser = null
            };

            var created = await _userRepo.CreateAsync(user);
            created.Senha = null;
            return created;
        }

        public async Task<User> LoginAsync(string email, string senha)
        {
            var user = await _userRepo.GetByEmailAsync(email);
            if (user == null) return null;
            
            using var sha = SHA256.Create();
            var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(senha));
            var senhaHash = Convert.ToBase64String(hash);

            if (user.Senha != senhaHash) return null;

            user.Senha = null;
            return user;
        }
    }
}
