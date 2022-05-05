using AutoMapper;
using elearning_platform.Auth;
using elearning_platform.DTO;
using elearning_platform.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace elearning_platform.Controllers
{
    [ApiController]
    [Route("tutor_request")]
    public class TutorRequestController : ControllerBase
    {
        private readonly ISessionService _sessionService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public TutorRequestController(ISessionService sessionService, ICurrentUserService currentUserService, IMapper mapper)
        {
            _sessionService = sessionService;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        [HttpPut]
        [Authorize(Policy = Policies.StudentOnly)]
        [Route("{tutorRequestId}")]
        public ActionResult UpdateTutorRequest(Guid tutorRequestId, StudentUpdateTutorRequest updateTutorRequest)
        {
            var user = _currentUserService.User;
            var tutorRequest = _sessionService.UpdateTutorRequest(tutorRequestId, user!, updateTutorRequest);
            return Ok(tutorRequest);
        }

        [HttpPut]
        [Authorize(Policy = Policies.StudentOnly)]
        [Route("{tutorRequestId}/reply")]
        public ActionResult RespondToTutorRequest(Guid tutorRequestId, TutorUpdateTutorRequest updateTutorRequest)
        {
            var user = _currentUserService.User;
            var tutorRequest = _sessionService.UpdateTutorRequest(tutorRequestId, user!, updateTutorRequest);
            return Ok(tutorRequest);
        }
    }
}