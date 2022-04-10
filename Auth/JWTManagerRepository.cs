using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using elearning_platform.Models;
using elearning_platform.Configs;
using elearning_platform.DTO;
using elearning_platform.Repo;
using elearning_platform.Services;

namespace elearning_platform.Auth
{
    public class JWTManagerRepository : IJWTManagerRepository
    {
        public string[] users = { "user1", "user2" };
        private readonly JWTConfig _jwtConfig;
        private readonly IUserRepo _userRepo;
        private readonly IMfaRepo _mfaRepo;
        private readonly IAuthService _authService;
        private readonly IEmailService _emailService;

        public enum AuthResult { DOPE };

        public JWTManagerRepository(JWTConfig jwtConfig, IUserRepo userRepo, IMfaRepo mfaRepo, IAuthService authService)
        {
            _jwtConfig = jwtConfig;
            _userRepo = userRepo;
            _mfaRepo = mfaRepo;
            _authService = authService;
        }

        public async Task<JWTToken?> Authenticate(LoginDTO loginDTO)
        {
            var user = _userRepo.GetUserByUsername(loginDTO.Username, true);
            if (user != null)
            {
                if (loginDTO.pinCode != null)

                {
                    var authenticated = await _mfaRepo.CheckMfaAsync(user, (int)loginDTO.pinCode);
                    if (authenticated)
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
                    throw new Exception("Wrong Multi Factor Auth");
                }
                else
                {
                    var code = await _mfaRepo.GenerateMfaAsync(user);
                    _mfaRepo.SaveChanges();
                    var success = await _authService.SendMfaAsync(user, code);
                    return null;
                }
            }
            throw new Exception("User doesn't exist");
        }
    }
}