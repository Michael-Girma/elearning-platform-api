using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using elearning_platform.Auth;
using elearning_platform.Services;
using elearning_platform.DTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
            return Ok(_mapper.Map<IEnumerable<ReadTaughtSubjectDTO>>(_taughtSubjectService.GetTaughtSubjectBySid(subjectId)));
        }

        [HttpPost]
        [Route("{subjectId}/star")]
        [Authorize]
        public ActionResult StarSubjectForUser(Guid subjectId, bool star)
        {
            var user = _currentUserService.User;
            var starredSubject = _subjectService.StarSubject(user!, subjectId);
            return Ok(_mapper.Map<ReadStarredSubject>(starredSubject));
        }

        [HttpPost]
        [Route("{subjectId}/unstar")]
        [Authorize]
        public ActionResult UnstarSubjectForUser(Guid subjectId, bool star)
        {
            var user = _currentUserService.User;
            return Ok(_subjectService.UnstarSubject(user!, subjectId));
        }

        [HttpGet]
        [Route("get_starred_subjects")]
        [Authorize]
        public ActionResult GetStarredSubjects(Guid subjectId)
        {
            var user = _currentUserService.User;
            var starredSubjects = _subjectService.GetStarredSubjectsForUser(user!);
            {return Ok(starredSubjects.ToList());}
            //TODO: MAP TO DTO THIS IS NOT SAFE
        }

        [HttpGet]
        [Route("all")]
        public ActionResult GetAllSubjects(Guid subjectId)
        {
            return Ok(_mapper.Map<IEnumerable<ReadSubjectDTO>>(_subjectService.GetAllSubjects().ToList()));
        }

        [HttpGet]
        [Route("{subjectId}")]
        public ActionResult GetSubjectById(Guid subjectId)
        {
            return Ok(_mapper.Map<ReadSubjectDTO>(_subjectService.GetSubjectById(subjectId)));
        }

        [HttpGet]
        [Route("levels")]
        public ActionResult GetEducationLevels(Guid subjectId)
        {
            return Ok(_subjectService.GetEducationLevels().ToList());
        }

        [HttpPost]
        [Route("request")]
        [Authorize(Policy=Policies.TutorOnly)]
        public ActionResult RequestSubject(CreateSubjectRequestDTO createSubjectRequestDTO)
        {
            var tutor = _currentUserService.GetTutor();
            return Ok(_mapper.Map<ReadSubjectRequestDTO>(_subjectService.RequestSubject(tutor, createSubjectRequestDTO)));
        }

        [HttpGet]
        [Route("requests")]
        [Authorize(Policy=Policies.AdminOnly)]
        public ActionResult GetAllSubjectRequests()
        {
            return Ok(_mapper.Map<IEnumerable<ReadSubjectRequestDTO>>(_subjectService.GetAllSubjectRequests()));
        }

        [HttpPost]
        [Route("requests/{requestId}/{response}")]
        [Authorize(Policy=Policies.AdminOnly)]
        public ActionResult GetAllSubjectRequests(Guid requestId, bool response)
        {
            return Ok(_mapper.Map<ReadSubjectRequestDTO>(_subjectService.AddressSubjectRequest(requestId, response)));
        }

        [HttpPut]
        [Route("{subjectId}")]
        [Authorize(Policy = Policies.AdminOnly)]
        public ActionResult EditSubjectDetails(Guid subjectId, CreateSubjectDTO dto)
        {
            return Ok(_mapper.Map<ReadSubjectDTO>(_subjectService.EditSubject(subjectId, dto)));
        }
    }
}