using AutoMapper;
using elearning_platform.Auth;
using elearning_platform.DTO;
using elearning_platform.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace elearning_platform.Controllers
{
    [ApiController]
    [Route("feedback")]
    public class FeedbackController : ControllerBase
    {
        private readonly ISessionService _sessionService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public FeedbackController(ISessionService sessionService, ICurrentUserService currentUserService, IMapper mapper)
        {
            _sessionService = sessionService;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Policy = Policies.TutorOnly)]
        [Route("tutor")]
        public ActionResult GetFeedbacksForTutor()
        {
            var tutor = _currentUserService.GetTutor();
            var feedbacks = _sessionService.GetFeedbacksOfTutor(tutor.TutorId);
            return Ok(_mapper.Map<IEnumerable<ReadSessionFeedbackDTO>>(feedbacks));
        }

        [HttpGet]
        [Authorize(Policy = Policies.StudentOnly)]
        [Route("student")]
        public ActionResult GetFeedbacksForStudent()
        {
            var student = _currentUserService.GetStudent();
            var feedbacks = _sessionService.GetFeedbacksOfStudent(student.StudentId).ToList();
            return Ok(_mapper.Map<IEnumerable<ReadSessionFeedbackDTO>>(feedbacks));
        }

        [HttpGet]
        [Authorize(Policy = Policies.AdminOnly)]
        [Route("all_reports")]
        public ActionResult GetAllReports()
        {
            var feedbacks = _sessionService.GetAllReports().ToList();
            return Ok(_mapper.Map<IEnumerable<ReadSessionFeedbackDTO>>(feedbacks));
        }

        [HttpPost]
        [Authorize(Policy = Policies.AdminOnly)]
        [Route("{feedbackId}/mark_as_addressed")]
        public ActionResult MarkAsAddressed(Guid feedbackId)
        {
            var feedback = _sessionService.MarkAsAddressed(feedbackId);
            return Ok(_mapper.Map<ReadSessionFeedbackDTO>(feedback));
        }
    }
}