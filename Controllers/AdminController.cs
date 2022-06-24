using AutoMapper;
using elearning_platform.Auth;
using elearning_platform.DTO;
using elearning_platform.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace elearning_platform.Controllers
{
    [ApiController]
    [Route("admin")]
    public class AdminController : ControllerBase
    {
        private readonly IOnboardingService _onboardingService;
        private readonly ISubjectService _subjectService;
        private readonly IMapper _mapper;
        public AdminController(IOnboardingService onboardingService, IMapper mapper, ISubjectService subjectService)
        {
            _onboardingService = onboardingService;
            _mapper = mapper;
            _subjectService = subjectService;
        }

        [HttpGet]
        [Route("users/all")]
        [Authorize(Policy=Policies.AdminOnly)]
        public ActionResult GetAllUsers()
        {
            return Ok(_mapper.Map<IEnumerable<ReadUserDTO>>(_onboardingService.GetAllUsers().ToList()));
        }

        [HttpPost]
        [Route("users/{userId}/ban/{banned}")]
        [Authorize(Policy=Policies.AdminOnly)]
        public ActionResult BanUser(Guid userId, bool banned)
        {
            return Ok(_mapper.Map<ReadUserDTO>(_onboardingService.setUserBan(userId, banned)));
        }

        [HttpPost]
        [Route("education_levels/{educationName}")]
        [Authorize(Policy=Policies.AdminOnly)]
        public ActionResult CreateEducationLevel(string educationName)
        {
            return Ok(_subjectService.AddEducationLevel(educationName));
        }
    }
}