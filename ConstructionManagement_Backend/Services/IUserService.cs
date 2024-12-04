using ConstructionManagement_Backend.Models;

namespace ConstructionManagement_Backend.Services
{
    public interface IUserService
    {
        Task<bool> RegisterUserAsync(UserRegisterRequest user);
        Task<string> AuthenticateUserAsync(string username, string password);
        Task<List<User>> GetAllUsersAsync();
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(string id);
    }
}
