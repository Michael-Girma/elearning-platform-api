using System.Text;
using AutoMapper;
using elearning_platform.DTO;
using elearning_platform.Models;
using elearning_platform.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace elearning_platform.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IOnboardingService _onboardingService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IAuthService _authService; 
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UserController(IUserService userService, IOnboardingService onboardingService, IMapper mapper, ICurrentUserService currentUserService, IAuthService authService)
        {
            _onboardingService = onboardingService;
            _currentUserService = currentUserService;
            _authService = authService;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpPut]
        [Authorize]
        [Route("update")]
        public async Task<ActionResult> UpdateUserDetails(UpdateUserDetailsDTO updateUserDetailDTO)
        {
            var user = _currentUserService.User;
            return Ok(_onboardingService.UpdateUserDetails(user.Uid, updateUserDetailDTO));
        }

        [HttpPut]
        [Authorize]
        [Route("update_payment")]
        public async Task<ActionResult> UpdatePaymentDetails(CreatePaymentAccountDetailDTO updatePaymentDetail)
        {
            var user = _currentUserService.User;
            return Ok(_userService.UpdatePaymentDetails(user.Uid, updatePaymentDetail));
        }


        [HttpPost]
        [Authorize]
        [Route("change_password")]
        public async Task<ActionResult> ChangePassword(ChangePasswordDTO passwordDTO)
        {
            var user = _currentUserService.User;
            return Ok(_mapper.Map<ReadUserDTO>(_authService.ChangePassword(user, passwordDTO)));
        }

        [HttpGet]
        [Authorize]
        [Route("user_details/{uid}")]
        public async Task<ActionResult> GetUserDetails(Guid uid)
        {
            // var user = _currentUserService.User;
            return Ok(_mapper.Map<ReadUserDTO>(_userService.GetUserDetails(uid)));
        }
    }
}