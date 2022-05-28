using elearning_platform.Auth;
using elearning_platform.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace elearning_platform.Controllers
{
    [ApiController]
    [Route("activity")]
    public class ActivityController: ControllerBase
    {
        private readonly IStatService _statService;
        private readonly ICurrentUserService _currentUserService;

        public ActivityController(IStatService statService, ICurrentUserService currentUserService)
        {
            _statService = statService;
            _currentUserService = currentUserService;
        }

        [HttpGet]
        [Authorize(Policy=Policies.StudentOnly)]
        public ActionResult GetStudentActivity()
        {
            var student = _currentUserService.GetStudent();

            return Ok(_statService.GetStudentActivity(student.StudentId));
        } 
    }
}