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
    [Route("student")]
    public class StudentController : ControllerBase
    {
        private readonly ITutorRequestService _tutorRequestService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IStatService _statService;
        private readonly IMapper _mapper;

        public StudentController(IStatService statService, ICurrentUserService currentUserService, IMapper mapper, ITutorRequestService tutorRequestService)
        {
            _statService = statService;
            _tutorRequestService = tutorRequestService;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("tutor_requests")]
        [Authorize(Policy=Policies.StudentOnly)]
        public async Task<ActionResult> GetTutorsEnquiried()
        {
            var student = _currentUserService.GetStudent();
            var requests = _tutorRequestService.GetRequestsForStudent(student.StudentId);
            return Ok(_mapper.Map<IEnumerable<ReadTutorRequestDTO>>(requests));
        }
    }
}