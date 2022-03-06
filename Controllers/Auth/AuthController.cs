using elearning_platform.Auth;
using elearning_platform.DTO;
using elearning_platform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using elearning_platform.Repo;

namespace elearning_platform.Controllers.Auth
{
    [Route("Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJWTManagerRepository _jwtRepo;
        private readonly IUserRepo _userRepo;

        public AuthController(IJWTManagerRepository jwtRepo, IUserRepo userRepo)
        {
            _jwtRepo = jwtRepo;
            _userRepo = userRepo;
        }

        [HttpPost]
        [Route("login")]
        public ActionResult<JWTToken> login(LoginDTO readUserDto)
        {
            var token = _jwtRepo.Authenticate(readUserDto);
            return Ok(token);
        }
    }
}