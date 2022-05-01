using elearning_platform.Models;
using elearning_platform.Services;
using elearning_platform.ExtensionMethods.Auth;

namespace elearning_platform.Middlewares
{
    public class UserAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public UserAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ICurrentUserService currentUserService)
        {

            var uid = context.User.GetUserId();
            if (uid != null)
            {
                currentUserService.SetUser(uid ?? Guid.Empty);
            }

            await _next(context);
        }
    }

    public static class UserAuthMiddlewareExtensions
    {
        public static IApplicationBuilder UseCurrentUserService(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UserAuthMiddleware>();
        }
    }

}