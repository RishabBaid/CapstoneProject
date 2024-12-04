using ConstructionManagement_Backend.Models;
using MongoDB.Driver;

namespace ConstructionManagement_Backend.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(IMongoDatabase database)
        {
            _users = database.GetCollection<User>("Users");
        }

        public async Task CreateUserAsync(User user) => await _users.InsertOneAsync(user);

        public async Task<User> GetUserByIdAsync(string id) =>
            await _users.Find(u => u.Id == id).FirstOrDefaultAsync();

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _users.Find(u => u.Username == username).FirstOrDefaultAsync();
        }

        public async Task<List<User>> GetAllUsersAsync() => await _users.Find(_ => true).ToListAsync();

        public async Task UpdateUserAsync(User user) =>
            await _users.ReplaceOneAsync(u => u.Id == user.Id, user);

        public async Task DeleteUserAsync(string id) =>
            await _users.DeleteOneAsync(u => u.Id == id);
    }
}
