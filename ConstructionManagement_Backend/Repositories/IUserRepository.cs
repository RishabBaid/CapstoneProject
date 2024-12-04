using ConstructionManagement_Backend.Models;

namespace ConstructionManagement_Backend.Repositories
{
    public interface IUserRepository
    {
        Task CreateUserAsync(User user);
        Task<User> GetUserByIdAsync(string id);
        Task<User> GetUserByUsernameAsync(string username);
        Task<List<User>> GetAllUsersAsync();
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(string id);
    }
}
