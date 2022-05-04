using Microsoft.AspNetCore.Mvc;
using elearning_platform.DTO;
using elearning_platform.Services;
using AutoMapper;
using elearning_platform.Auth;
using Microsoft.AspNetCore.Authorization;

namespace elearning_platform.Controllers
{
    [ApiController]
    [Route("taught_subject")]
    public class TaughtSubjectController : ControllerBase
    {
        private readonly ISessionService _sessionService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public TaughtSubjectController(ISessionService sessionService, IMapper mapper, ICurrentUserService currentUserService)
        {
            _sessionService = sessionService;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }



        [HttpPost]
        [Authorize(Policy = Policies.StudentOnly)]
        [Route("request")]
        public ActionResult RequestTutor(CreateTutorRequestDTO createTutorRequestDTO)
        {
            var student = _currentUserService.GetStudent();
            var tutorRequestModel = _sessionService.CreateTutorRequest(student!, createTutorRequestDTO);
            return Ok(tutorRequestModel);
        }
    }
}