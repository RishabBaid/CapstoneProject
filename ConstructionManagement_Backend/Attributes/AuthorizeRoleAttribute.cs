using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ConstructionManagement_Backend.Attributes
{
    public class AuthorizeRoleAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _role;

        public AuthorizeRoleAttribute(string role)
        {
            _role = role;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userRole = context.HttpContext.Items["UserRole"]?.ToString();

            if (userRole == null || !_role.Split(',').Contains(userRole, StringComparer.OrdinalIgnoreCase))
            {
                // User is not authorized
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status403Forbidden };
            }
        }

    }
}
