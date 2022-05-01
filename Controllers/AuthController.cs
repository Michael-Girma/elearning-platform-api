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
        private readonly IOnboardingService _onboardingService;
        private readonly IStudentRepo _studentRepo;
        private readonly IMapper _mapper;
        private readonly IClaimRepo _claimRepo;
        private readonly IAdminRepo _adminRepo;
        private readonly ICurrentUserService _currentUserService;
        private readonly IFileService _fs;
        private readonly ITutorRepo _tutorRepo;

        public AuthController(IJWTManagerRepository jwtRepo, ITutorRepo tutorRepo, IFileService fs, IUserRepo userRepo, IStudentRepo studentRepo, IAdminRepo adminRepo, IMapper mapper, IAuthService authService, IClaimRepo claimRepo, ICurrentUserService currentUserService, IOnboardingService onboardingService)
        {
            _jwtRepo = jwtRepo;
            _authService = authService;
            _currentUserService = currentUserService;
            _onboardingService = onboardingService;
            _userRepo = userRepo;
            _studentRepo = studentRepo;
            _mapper = mapper;
            _adminRepo = adminRepo;
            _claimRepo = claimRepo;
            _fs = fs;
            _tutorRepo = tutorRepo;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> login(LoginDTO readUserDto)
        {
            //TODO: change route to verify email addresses
            var auth = await _jwtRepo.Authenticate(readUserDto);
            if (auth.Result == AuthResult.MfaCodeIssued)
            {
                return Ok("Please check your email for MFA code");
            }
            return Ok(auth.JwtToken);
        }

        [HttpPost]
        [Route("student/login")]
        public async Task<ActionResult<LoginStudentDTO>> LoginStudent(LoginDTO readUserDto)
        {
            try
            {
                var auth = await _jwtRepo.Authenticate(readUserDto);
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
            try
            {
                var auth = await _jwtRepo.Authenticate(readUserDto);
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

        [HttpPost]
        [Authorize]
        [Route("student/signup")]
        public ActionResult OnboardStudent(StudentSignupDTO studentSignupDto)
        {
            var studentModel = _mapper.Map<StudentSignupDTO, Student>(studentSignupDto);
            studentModel.Uid = _currentUserService.User.Uid;

            var student = _studentRepo.CreateStudent(studentModel);
            student = _claimRepo.AddClaimForStudent(student);
            _studentRepo.SaveChanges();
            return Ok(student);
        }

        [HttpPost]
        [Route("tutor/signup")]
        public ActionResult OnboardTutor(SignupDTO signupDTO)
        {
            try
            {
                var tutorModel = _onboardingService.SignupTutor(signupDTO);
                return Ok(tutorModel);
            }
            catch (BadRequestException e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost]
        [Route("tutor/login")]
        public async Task<ActionResult<LoginAdminDTO>> LoginTutor(LoginDTO readUserDto)
        {
            try
            {
                var auth = await _jwtRepo.Authenticate(readUserDto);
                if (auth.Result == AuthResult.MfaCodeIssued)
                {
                    return Ok("Please check your email for MFA code");
                }
                else
                {
                    var token = auth.JwtToken;
                    var admin = _tutorRepo.GetTutorByUid(token.Uid);
                    if (admin == null)
                    {
                        return Forbid("UserData Not Found");
                    }
                    var loginDTO = new ReadLoginTutorDTO { Tutor = admin, Auth = token };

                    return Ok(loginDTO);
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



        [HttpGet]
        [Route("ping")]
        public StudentSignupDTO Ping()
        {
            return new StudentSignupDTO()
            {
                DateOfBirth = DateTime.Now
            };
        }

        [HttpPost]
        [Route("signup")]
        public ActionResult SignupUser(SignupDTO signupDTO)
        {
            try
            {
                var userModel = _onboardingService.SignupUser(signupDTO);
                return Ok(userModel);
            }
            catch (BadRequestException e)
            {
                return BadRequest(e);
            }
        }


    }
}