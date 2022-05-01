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
        private readonly IMapper _mapper;

        public SubjectController(ISubjectService subjectService, ICurrentUserService currentUserService, IMapper mapper)
        {
            _subjectService = subjectService;
            _currentUserService = currentUserService;
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
    }
}