using PetaPoco;
using StagyTimeApi.Models;
using StagyTimeApi.Repositories.Interfaces;

namespace StagyTimeApi.Repositories
{

    public class UserRepository : IUserRepository
    {
        private readonly Database _db;
        public UserRepository(Database db) => _db = db;

        public async Task<User> CreateAsync(User user)
        {
            var id = await _db.InsertAsync("User","Id", user);
            user.Id = Convert.ToInt32(id);
            return user;
        }
        public async Task<User> GetByEmailAsync(string email)
        {
            return await _db.SingleOrDefaultAsync<User>("SELECT * FROM User WHERE Email = @0", email);
        }
    }


}