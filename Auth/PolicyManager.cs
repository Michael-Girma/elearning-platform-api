using elearning_platform.Repo;
using Microsoft.AspNetCore.Authorization;

namespace elearning_platform.Auth
{
    public class PolicyManager
    {
        public static void SetAuthorizationPolicies(AuthorizationOptions options)
        {
            options.AddPolicy(Policies.StudentOnly, policy => policy.RequireClaim("STUDENT"));
            options.AddPolicy(Policies.AdminOnly, policy => policy.RequireClaim("ADMIN"));
            options.AddPolicy(Policies.TutorOnly, policy => policy.RequireClaim("TUTOR"));
            options.AddPolicy(Policies.TutorOrAdmin, policyBuilder => policyBuilder.RequireAssertion(
                context => context.User.HasClaim(claim =>
                            claim.Type == "TUTOR"
                            || claim.Type == "ADMIN"))
            );
        }
    }

    public static class Policies
    {
        public const string StudentOnly = "StudentOnly";
        public const string AdminOnly = "AdminOnly";
        public const string TutorOnly = "TutorOnly";
        public const string TutorOrAdmin = "TutorOrAdmin";
    }
}