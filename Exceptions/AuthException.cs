using elearning_platform.Models;

namespace elearning_platform.Exceptions
{
    public class AuthException : Exception
    {
        public AuthResult Result;
        public AuthException(AuthResult result) : base(AuthResponse.GetResultString(result))
        {
            Result = result;
        }
    }
}