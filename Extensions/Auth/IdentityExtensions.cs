using System.Security.Claims;
using System.Security.Principal;

namespace elearning_platform.ExtensionMethods.Auth
{
    public static class IdentityExtensions
    {
        public static int? GetUserId(this ClaimsPrincipal user)
        {
            var userClaim = user.Claims.Where<Claim>(claim => claim.Type == ClaimTypes.UserData).FirstOrDefault<Claim>();
            if (userClaim == null) return null;
            int userId;
            var parsingSuccess = Int32.TryParse(userClaim.Value, out userId);
            return userId;
        }
    }
}