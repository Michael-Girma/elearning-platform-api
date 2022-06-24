using elearning_platform.Auth;
using elearning_platform.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace elearning_platform.Controllers
{
    [ApiController]
    [Route("activity")]
    public class ActivityController : ControllerBase
    {
        private readonly IStatService _statService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IPaymentService _paymentService;

        public ActivityController(IPaymentService paymentService, IStatService statService, ICurrentUserService currentUserService)
        {
            _statService = statService;
            _currentUserService = currentUserService;
            _paymentService = paymentService;
        }

        [HttpGet]
        [Route("student")]
        [Authorize(Policy=Policies.StudentOnly)]
        public ActionResult GetStudentActivity()
        {
            var student = _currentUserService.GetStudent();

            return Ok(_statService.GetStudentActivity(student.StudentId));
        }

        [HttpGet]
        [Route("tutor")]
        [Authorize(Policy=Policies.TutorOnly)]
        public ActionResult GetTutorActivity()
        {
            var tutor = _currentUserService.GetTutor();
            return Ok(_statService.GetTutorActivity(tutor.TutorId));
        }

        [HttpGet]
        [Route("platform")]
        [Authorize(Policy=Policies.AdminOnly)]
        public ActionResult GetPlatformOverview()
        {
            return Ok(_statService.GetPlatformOverview());
        }

        [HttpGet]
        [Route("payments")]
        [Authorize(Policy=Policies.AdminOnly)]
        public ActionResult GetPayments()
        {
            return Ok(_paymentService.GetAllPayments());
        }
    }
}