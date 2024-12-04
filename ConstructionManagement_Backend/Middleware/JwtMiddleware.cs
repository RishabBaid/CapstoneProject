using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionManagement_Backend.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public JwtMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                await AttachUserToContext(context, token);

            await _next(context);
        }

        private async Task AttachUserToContext(HttpContext context, string token)
        {
            try
            {
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
                var tokenHandler = new JwtSecurityTokenHandler();

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userRole = jwtToken.Claims.First(x => x.Type == "role")?.Value;

                // Attach role to the HTTP context
                context.Items["UserRole"] = userRole;
            }
            catch
            {
                // Do nothing if the JWT validation fails
                // The user won't be attached to the context, so they'll be unauthorized
            }
        }
    }
}
