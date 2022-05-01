using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using elearning_platform.Auth;

namespace elearning_platform.Controllers
{
    [ApiController]
    [Route("subjects")]
    public class SubjectController : ControllerBase
    {
        private const string policyname = "TUTOR";

        [HttpPost]
        [Route("create")]
        [Authorize(Policy = Policies.TutorOrAdmin)]
        public ActionResult AddSubject()
        {
            return Ok("Route Guarded");
        }
    }
}