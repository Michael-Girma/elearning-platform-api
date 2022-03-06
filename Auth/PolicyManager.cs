using elearning_platform.Repo;
using Microsoft.AspNetCore.Authorization;

namespace elearning_platform.Auth
{
    public class PolicyManager
    {
        public static void SetAuthorizationPolicies(AuthorizationOptions options)
        {
            options.AddPolicy("StudentOnly", policy => policy.RequireClaim("UserType", "Student"));
            options.AddPolicy("AdminOnly", policy => policy.RequireClaim("UserType", "Admin"));
        }
    }
}