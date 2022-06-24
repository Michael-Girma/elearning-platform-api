using AutoMapper;
using elearning_platform.Auth;
using elearning_platform.DTO;
using elearning_platform.Models;
using elearning_platform.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace elearning_platform.Controllers
{
    [ApiController]
    [Route("Sessions")]
    public class SessionController : ControllerBase
    {   
        private readonly ISessionService _sessionService; 
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public SessionController(ISessionService sessionService, IMapper mapper, ICurrentUserService currentUserService)
        {
            _sessionService = sessionService;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        [HttpPost]
        [Route("book")]
        public async Task<ActionResult> BookSession(ReadPaymentDetailDTO paymentDetailDTO)
        {
            var paymentDetail = _mapper.Map<PaymentDetail>(paymentDetailDTO);
            var session = await _sessionService.BookSession(paymentDetail);
            // var projection = _mapper.ProjectTo();

            return Ok(_mapper.Map<ReadSessionDTO>(session));
        }

        [HttpGet]
        [Route("all")]
        [Authorize]
        public async Task<ActionResult> GetAllSessions()
        {
            var user = _currentUserService.User;
            var sessions = _sessionService.GetAllSessionsForUser(user!.Uid);
            return Ok(_mapper.Map<List<ReadSessionDTO>>(sessions.ToList()));
        }

        [HttpGet]
        [Route("{sessionId}")]
        [Authorize]
        public async Task<ActionResult> GetSessionById(Guid sessionId)
        {
            var user = _currentUserService.User;
            var session = _sessionService.GetAllSessionsForUser(user!.Uid).FirstOrDefault(e => e.SessionId == sessionId);
            return Ok(_mapper.Map<ReadSessionDTO>(session));
        }

        [HttpPost]
        [Authorize(Policy = Policies.StudentOnly)]
        [Route("{sessionId}/generate_link")]
        public ActionResult BookingPaymentLink(Guid sessionId, CreatePaymentLinkDTO paymentLinkDTO)
        {
            var student = _currentUserService.GetStudent();
            var paymentLink = _sessionService.GenerateLinkForSession(sessionId, student!, paymentLinkDTO);
            return Ok(_mapper.Map<ReadPaymentLinkDTO>(paymentLink));
        }

        [HttpPost]
        [Authorize(Policy = Policies.StudentOnly)]
        [Route("{sessionId}/leave_feedback")]
        public ActionResult LeaveFeedback(Guid sessionId, CreateSessionFeedbackDTO feedbackDTO)
        {
            var student = _currentUserService.GetStudent();
            var feedback = _sessionService.LeaveFeedback(student!, sessionId, feedbackDTO);
            return Ok(_mapper.Map<ReadSessionFeedbackDTO>(feedback));
        }

        // [HttpPost]
        // [Authorize(Policy = Policies.TutorOnly)]
        // [Route("{sessionId}/assessments")]
        // public ActionResult AddAssessment()
        // {
        //     return Ok();
        // }

        [HttpPost]
        [Authorize(Policy = Policies.TutorOnly)]
        [Route("{sessionId}/resources")]
        public ActionResult AddResource(CreateResourceDTO resourceDTO, Guid sessionId)
        {
            var tutor = _currentUserService.GetTutor();
            var resource = _sessionService.UploadResource(tutor, sessionId, resourceDTO);
            return Ok(_mapper.Map<ReadResourceDTO>(resource));
        }

        [HttpGet]
        [Route("insights")]
        [Authorize(Policy=Policies.StudentOnly)]
        public async Task<ActionResult> GetTutorsEnquiried()
        {
            var student = _currentUserService.GetStudent();
            var requestInsights = await _sessionService.GetAllEnquiryInsights(student.StudentId);
            return Ok(requestInsights);
        }

        [HttpPost]
        [Authorize(Policy=Policies.StudentOnly)]
        [Route("{sessionId}/student_notes")]
        public async Task<ActionResult> UpdateStudentNotes(Guid sessionId, UpdateStudentNotesDTO notesDTO)
        {
            var student = _currentUserService.GetStudent();
            var updatedSession = _sessionService.UpdateStudentNotes(student, sessionId, notesDTO);
            return Ok(_mapper.Map<ReadSessionDTO>(updatedSession));
        }

        [HttpPost]
        [Authorize(Policy=Policies.TutorOnly)]
        [Route("{sessionId}/recommendations")]
        public async Task<ActionResult> UpdateRecommendations(Guid sessionId, UpdateRecommendationsDTO notesDTO)
        {
            var tutor = _currentUserService.GetTutor();
            var updatedSession = _sessionService.UpdateRecommendations(tutor, sessionId, notesDTO);
            return Ok(_mapper.Map<ReadSessionDTO>(updatedSession));
        }

        [HttpDelete]
        [Authorize(Policy=Policies.TutorOnly)]
        [Route("resources/{resourceId}")]
        public async Task<ActionResult> DeleteResource(Guid resourceId)
        {
            var tutor = _currentUserService.GetTutor();
            var updatedSession = _sessionService.RemoveResource(tutor, resourceId);
            return NoContent();
        }

        [HttpDelete]
        [Authorize(Policy=Policies.TutorOnly)]
        [Route("assessments/{assessmentId}")]
        public async Task<ActionResult> DeleteAssessment(Guid assessmentId)
        {
            var tutor = _currentUserService.GetTutor();
            var updatedSession = _sessionService.RemoveAssessment(tutor, assessmentId);
            return NoContent();
        }

        [HttpPost]
        [Authorize(Policy=Policies.TutorOnly)]
        [Route("{sessionId}/assessments")]
        public async Task<ActionResult> AddAssessment(CreateAssessmentDTO assessmentDTO, Guid sessionId)
        {
            var tutor = _currentUserService.GetTutor();
            var updatedSession = _sessionService.AddAssessment(tutor, sessionId, assessmentDTO);
            return Ok(updatedSession);
        }

    }
}