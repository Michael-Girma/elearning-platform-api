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
        private readonly ITaughtSubjectService _taughtSubjectService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public TaughtSubjectController(ISessionService sessionService, IMapper mapper, ICurrentUserService currentUserService, ITaughtSubjectService subjectService)
        {
            _sessionService = sessionService;
            _currentUserService = currentUserService;
            _taughtSubjectService = subjectService;
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

        [HttpPut]
        [Authorize(Policy=Policies.TutorOnly)]
        [Route("{taughtSubjectId}")]
        public ActionResult UpdateTaughtSubject(Guid taughtSubjectId, UpdateTaughtSubjectDTO requestDTO)
        {
            var tutor = _currentUserService.GetTutor();
            return Ok(_mapper.Map<ReadTaughtSubjectDTO>(_taughtSubjectService.UpdateTaughtSubject(tutor!.TutorId, taughtSubjectId, requestDTO)));
        }

        [HttpGet]
        [Route("all")]
        [Authorize(Policy = Policies.TutorOnly)]
        public ActionResult GetAllTaughtSubjects()
        {
            var tutor = _currentUserService.GetTutor();
            return Ok(_mapper.Map<IEnumerable<ReadTaughtSubjectDTO>>(_taughtSubjectService.GetTaughtSubjectsForTutor(tutor!.TutorId).Where(e => !e.Deleted).ToList()));
        }

        [HttpGet]
        [Route("all_lessons")]
        [Authorize(Policy = Policies.AdminOnly)]
        public ActionResult GetAllLessons()
        {
            return Ok(_mapper.Map<IEnumerable<ReadTaughtSubjectDTO>>(_taughtSubjectService.GetAllTaughtSubjects().ToList()));
        }

        [HttpDelete]
        [Authorize(Policy=Policies.TutorOnly)]
        [Route("{taughtSubjectId}")]
        public ActionResult DeleteTaughtSubject(Guid taughtSubjectId)
        {
            var tutor = _currentUserService.GetTutor();
            _taughtSubjectService.DeleteTaughtSubjectForTutor(tutor.TutorId, taughtSubjectId);
            return NoContent();
        }

        [HttpPost]
        [Authorize(Policy=Policies.AdminOnly)]
        [Route("{taughtSubjectId}/approval/{approval}")]
        public ActionResult ApproveTaughtSubject(Guid taughtSubjectId, bool approval)
        {
            return Ok(_mapper.Map<ReadTaughtSubjectDTO>(_taughtSubjectService.ApproveLesson(taughtSubjectId, approval)));

        }
    }
}