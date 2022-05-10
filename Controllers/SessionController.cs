using AutoMapper;
using elearning_platform.Auth;
using elearning_platform.DTO;
using elearning_platform.Models;
using elearning_platform.Services;
using Microsoft.AspNetCore.Mvc;

namespace elearning_platform.Controllers
{
    [ApiController]
    [Route("Sessions")]
    public class SessionController : ControllerBase
    {   
        private readonly ISessionService _sessionService; 
        private readonly IMapper _mapper;

        public SessionController(ISessionService sessionService, IMapper mapper)
        {
            _sessionService = sessionService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("book")]
        public async Task<ActionResult> BookSession(ReadPaymentDetailDTO paymentDetailDTO)
        {
            var paymentDetail = _mapper.Map<PaymentDetail>(paymentDetailDTO);
            var session = await _sessionService.BookSession(paymentDetail);
            // var projection = _mapper.ProjectTo();

            return Ok(session);
        }
    }
}