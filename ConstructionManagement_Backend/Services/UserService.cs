using ConstructionManagement_Backend.Models;
using ConstructionManagement_Backend.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ConstructionManagement_Backend.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<bool> RegisterUserAsync(UserRegisterRequest userRequest)
        {
            if (await _userRepository.GetUserByUsernameAsync(userRequest.Username) != null)
                return false;
            var user = new User();
            user.Username = userRequest.Username;
            user.Email = userRequest.Email;
            user.Role = userRequest.Role;
            user.PhoneNumber = userRequest.PhoneNumber;
            user.IsActive = userRequest.IsActive;
            //user.Id = Guid.NewGuid().ToString();
            user.Password = BCrypt.Net.BCrypt.HashPassword(userRequest.Password);
            await _userRepository.CreateUserAsync(user);
            return true;
        }

        public async Task<string> AuthenticateUserAsync(string username, string password)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
                return null;

            // Generate JWT token after successful authentication
            return GenerateJwtToken(user);
        }

        public async Task<List<User>> GetAllUsersAsync() => await _userRepository.GetAllUsersAsync();

        public async Task UpdateUserAsync(User user) => await _userRepository.UpdateUserAsync(user);

        public async Task DeleteUserAsync(string id) => await _userRepository.DeleteUserAsync(id);

        // Generate JWT token
        private string GenerateJwtToken(User user)
        {
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
