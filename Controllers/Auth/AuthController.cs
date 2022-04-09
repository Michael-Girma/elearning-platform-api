using elearning_platform.Auth;
using elearning_platform.DTO;
using elearning_platform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using elearning_platform.Repo;
using elearning_platform.Services;

namespace elearning_platform.Controllers.Auth
{
    [Route("Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJWTManagerRepository _jwtRepo;
        private readonly IUserRepo _userRepo;
        private readonly IAuthService _authService;

        public AuthController(IJWTManagerRepository jwtRepo, IUserRepo userRepo, IAuthService authService)
        {
            _jwtRepo = jwtRepo;
            _userRepo = userRepo;
            _authService = authService;
        }

        [HttpPost]
        [Route("login")]
        public ActionResult login(LoginDTO readUserDto)
        {

            _authService.SendMfaEmailAsync("mchlgirma@gmail.com", "23e1");
            var token = _jwtRepo.Authenticate(readUserDto);
            return Ok(token);
        }
    }
}