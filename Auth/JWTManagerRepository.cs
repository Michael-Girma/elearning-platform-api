using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using elearning_platform.Models;
using elearning_platform.Configs;
using elearning_platform.DTO;
using elearning_platform.Repo;

namespace elearning_platform.Auth
{
    public class JWTManagerRepository : IJWTManagerRepository
    {
        public string[] users = { "user1", "user2" };
        private readonly JWTConfig _jwtConfig;
        private readonly IUserRepo _userRepo;

        public JWTManagerRepository(JWTConfig jwtConfig, IUserRepo userRepo)
        {
            _jwtConfig = jwtConfig;
            _userRepo = userRepo;
        }

        public JWTToken? Authenticate(LoginDTO loginDTO)
        {
            var user = _userRepo.GetUserByUsername(loginDTO.Username, true);
            if (user != null)
            {
                var userClaims = user.Claims;
                List<Claim> claims = new List<Claim>();
                foreach (var userClaim in userClaims)
                {
                    claims.Add(new Claim(userClaim.Claim, userClaim.Value));
                }
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.UTF8.GetBytes(_jwtConfig.Key);
                var descriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims.ToList()),
                    Expires = DateTime.UtcNow.AddMinutes(_jwtConfig.ExpiryTime),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(descriptor);
                var jwtToken = new JWTToken { Token = tokenHandler.WriteToken(token), Uid = user.Uid };
                return jwtToken;
            }
            return null;
        }
    }
}