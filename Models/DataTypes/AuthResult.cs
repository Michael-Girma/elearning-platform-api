namespace elearning_platform.Models
{
    public enum AuthResult
    {
        UserDoesNotExist,
        WrongMfaCode,
        MfaCodeIssued,
        IncorrectCredentials,
        Successful,
        ExpiredMfa
    }

    public class AuthResponse
    {
        public JWTToken? JwtToken;

        public AuthResult Result;

        public AuthResponse(AuthResult result)
        {
            Result = result;
        }

        public AuthResponse(AuthResult result, JWTToken jwtToken)
        {
            Result = result;
            JwtToken = jwtToken;
        }


        override public string ToString()
        {
            return GetResultString(Result);
        }

        public static string GetResultString(AuthResult result)
        {
            switch (result)
            {
                case AuthResult.Successful: return "Authenticated Succesfully";
                case AuthResult.IncorrectCredentials: return "Incorrect Credentials";
                case AuthResult.MfaCodeIssued: return "MFA Code Has Been Sent To Email";
                case AuthResult.WrongMfaCode: return "MFA Code Is Incorrect";
                case AuthResult.ExpiredMfa: return "MFA Code has Expired";
                default: return "Unknown Error Occured";
            }

        }
    }
}