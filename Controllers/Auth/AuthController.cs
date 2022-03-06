using elearning_platform.Auth;
using elearning_platform.DTO;
using elearning_platform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using elearning_platform.Repo;
using AutoMapper;

namespace elearning_platform.Controllers.Auth
{
    [Route("Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJWTManagerRepository _jwtRepo;
        private readonly IUserRepo _userRepo;
        private readonly IStudentRepo _studentRepo;
        private readonly IMapper _mapper;

        public AuthController(IJWTManagerRepository jwtRepo, IUserRepo userRepo, IStudentRepo studentRepo, IMapper mapper)
        {
            _jwtRepo = jwtRepo;
            _userRepo = userRepo;
            _studentRepo = studentRepo;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("login")]
        public ActionResult<JWTToken> login(LoginDTO readUserDto)
        {
            var token = _jwtRepo.Authenticate(readUserDto);
            return Ok(token);
        }

        [HttpPost]
        [Route("student/login")]
        public ActionResult<LoginStudentDTO> LoginStudent(LoginDTO readUserDto)
        {
            var token = _jwtRepo.Authenticate(readUserDto);
            if (token == null)
            {
                return Forbid("User Not Found");
            }
            var student = _studentRepo.GetStudentByUid(token.Uid, true);
            if (student == null)
            {
                return Forbid("UserData Not Found");
            }

            var loginStudentDTO = new LoginStudentDTO { Student = student, Auth = token };

            return Ok(loginStudentDTO);
        }
    }
}