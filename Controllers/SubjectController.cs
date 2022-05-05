using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using elearning_platform.Auth;
using elearning_platform.Services;
using elearning_platform.DTO;
using AutoMapper;

namespace elearning_platform.Controllers
{
    [ApiController]
    [Route("subjects")]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;
        private readonly ICurrentUserService _currentUserService;
        private readonly ITaughtSubjectService _taughtSubjectService;
        private readonly IMapper _mapper;

        public SubjectController(ISubjectService subjectService, ICurrentUserService currentUserService, IMapper mapper, ITaughtSubjectService taughtSubjectService)
        {
            _subjectService = subjectService;
            _currentUserService = currentUserService;
            _taughtSubjectService = taughtSubjectService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("create")]
        [Authorize(Policy = Policies.TutorOrAdmin)]
        public ActionResult AddSubject(CreateSubjectDTO createSubjectDTO)
        {
            var user = _currentUserService.User;
            var createdSubject = _subjectService.CreateSubject(user!, createSubjectDTO);
            var readSubjectDTO = _mapper.Map<ReadSubjectDTO>(createdSubject);
            return Ok(readSubjectDTO);
        }

        [HttpPost]
        [Route("teach")]
        [Authorize(Policy = Policies.TutorOnly)]
        public ActionResult TeachSubject(CreateTaughtSubjectDTO createTaughtSubjectDTO)
        {
            var user = _currentUserService.User;
            var taughtSubject = _taughtSubjectService.CreateTaughtSubject(user!, createTaughtSubjectDTO);
            return Ok(_mapper.Map<ReadTaughtSubjectDTO>(taughtSubject));
        }

        [HttpGet]
        [Route("{subjectId}/taught_subjects")]
        [Authorize]
        public ActionResult GetTutorsForSubject(Guid subjectId)
        {
            return Ok(_taughtSubjectService.GetTaughtSubjectBySid(subjectId));
        }
    }
}