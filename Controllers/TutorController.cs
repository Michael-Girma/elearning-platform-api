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
    [Route("enquiries")]
    public class TutorController : ControllerBase
    {
        private readonly ISessionService _sessionService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IStatService _statService;
        private readonly IMapper _mapper;

        public TutorController(IStatService statService, ICurrentUserService currentUserService, IMapper mapper, ISessionService sessionService)
        {
            _statService = statService;
            _sessionService = sessionService;
            _currentUserService = currentUserService;
            _mapper = mapper;
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
    }
}