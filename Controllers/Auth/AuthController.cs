using elearning_platform.Auth;
using elearning_platform.DTO;
using elearning_platform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using elearning_platform.Repo;
using elearning_platform.Services;
using elearning_platform.Exceptions;
using AutoMapper;

namespace elearning_platform.Controllers.Auth
{
    [Route("Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJWTManagerRepository _jwtRepo;
        private readonly IUserRepo _userRepo;
        private readonly IAuthService _authService;
        private readonly IStudentRepo _studentRepo;
        private readonly IMapper _mapper;
        private readonly IAdminRepo _adminRepo;

        public AuthController(IJWTManagerRepository jwtRepo, IUserRepo userRepo, IStudentRepo studentRepo, IAdminRepo adminRepo, IMapper mapper, IAuthService authService)
        {
            _jwtRepo = jwtRepo;
            _userRepo = userRepo;
            _studentRepo = studentRepo;
            _authService = authService;
            _mapper = mapper;
            _adminRepo = adminRepo;
        }

        [HttpPost]
        [Route("login")]
        public ActionResult login(LoginDTO readUserDto)
        {

            // _authService.SendMfaAsync("mchlgirma@gmail.com", "23e1");
            var token = _jwtRepo.Authenticate(readUserDto);
            return Ok(token);
        }

        [HttpPost]
        [Route("student/login")]
        public async Task<ActionResult<LoginStudentDTO>> LoginStudent(LoginDTO readUserDto)
        {
            var auth = await _jwtRepo.Authenticate(readUserDto);
            try
            {
                if (auth.Result == AuthResult.MfaCodeIssued)
                {
                    return Ok("Please check your email for MFA code");
                }
                else
                {
                    var token = auth.JwtToken;
                    var student = _studentRepo.GetStudentByUid(token.Uid, true);
                    if (student == null)
                    {
                        return Forbid("UserData Not Found");
                    }
                    var loginStudentDTO = new LoginStudentDTO { Student = student, Auth = token };

                    return Ok(loginStudentDTO);
                }
            }
            catch (AuthException e)
            {
                if (e.Result == AuthResult.WrongMfaCode)
                    return Forbid("Wrong MFA Code");
                else
                {
                    return Unauthorized(e.Message);
                }
            }
        }

        [HttpPost]
        [Route("admin/login")]
        public async Task<ActionResult<LoginAdminDTO>> LoginAdmin(LoginDTO readUserDto)
        {
            var auth = await _jwtRepo.Authenticate(readUserDto);
            try
            {
                if (auth.Result == AuthResult.MfaCodeIssued)
                {
                    return Ok("Please check your email for MFA code");
                }
                else
                {
                    var token = auth.JwtToken;
                    var admin = _adminRepo.GetAdminByUid(token.Uid);
                    if (admin == null)
                    {
                        return Forbid("UserData Not Found");
                    }
                    var loginAdminDTO = new LoginAdminDTO { Admin = admin, Auth = token };

                    return Ok(loginAdminDTO);
                }
            }
            catch (AuthException e)
            {
                if (e.Result == AuthResult.WrongMfaCode)
                    return Forbid(e.Result.ToString());
                else
                {
                    return Unauthorized(e.Result.ToString());
                }
            }
        }
    }
}