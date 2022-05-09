using AutoMapper;
using elearning_platform.DTO;
using elearning_platform.Exceptions;
using elearning_platform.Models;
using elearning_platform.Repo;
using Microsoft.EntityFrameworkCore;
using YenePaySdk;

namespace elearning_platform.Services
{
    public class SessionService : ISessionService
    {
        private readonly ITutorRequestRepo _tutorRequestRepo;
        private readonly ITaughtSubjectRepo _taughtSubjectRepo;
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;

        public SessionService(ITutorRequestRepo tutorRequestRepo, IMapper mapper, ITaughtSubjectRepo taughtSubjectRepo, IPaymentService paymentService)
        {
            _tutorRequestRepo = tutorRequestRepo;
            _taughtSubjectRepo = taughtSubjectRepo;
            _paymentService = paymentService;
            _mapper = mapper;
        }
        public TutorRequest? CreateTutorRequest(Student student, CreateTutorRequestDTO requestDTO)
        {
            var tutorRequestModel = _mapper.Map<TutorRequest>(requestDTO);
            tutorRequestModel.StudentId = student.StudentId;
            var taughtSubject = _taughtSubjectRepo.GetTaughtSubjectById(requestDTO.TaughtSubjectId);
            if (taughtSubject != null)
            {
                tutorRequestModel.Status = TutorRequest.RequestStatusValues.AwaitingTutor.ToString();
                _tutorRequestRepo.CreateTutorRequest(tutorRequestModel);
                return tutorRequestModel;
            }
            else
            {
                var statusDescription = "Selected Subject Doesn't Exist";
                var statusCode = "TAUGHT SUBJECT DOES NOT EXIST";
                throw new BadRequestException(statusDescription, statusCode);
            }
        }


        public TutorRequest UpdateTutorRequest(Guid id, User user, UpdateTutorRequestDTO updateTutorRequestDTO)
        {
            var request = _tutorRequestRepo.GetTutorRequestsForUser(user.Uid).FirstOrDefault(e => e.TutorRequestId == id);
            if (request == null)
            {
                throw new RequestUnauthorizedException("User isn't authorized to access resource");
            }
            var newTutorRequest = _mapper.Map<UpdateTutorRequestDTO, TutorRequest>(updateTutorRequestDTO, request);
            var savedModel = _tutorRequestRepo.UpdateRequest(newTutorRequest);
            return newTutorRequest;
        }

        public TutorRequest SetupAcceptedTutorRequest(Guid id, Tutor tutor)
        {
            var request = _tutorRequestRepo.GetTutorRequestsForTutor(tutor.TutorId).FirstOrDefault(e => e.TutorRequestId == id);
            if (request == null)
            {
                throw new RequestUnauthorizedException("User isn't authorized to access resource");
            }
            if (request.Status == TutorRequest.RequestStatusValues.Accepted.ToString())
            {
                throw new BadRequestException("Tutor Request Has Already Been Accepted");
            }
            request.Status = TutorRequest.RequestStatusValues.Accepted.ToString();
            request.Sessions = CreateSessionsFromTutorRequest(request);
            _tutorRequestRepo.UpdateRequest(request);
            return request;
        }

        public List<Session> CreateSessionsFromTutorRequest(TutorRequest tutorRequest)
        {
            var sessions = new List<Session>();
            foreach (var date in tutorRequest.PreferredDates)
            {
                var newSession = new Session()
                {
                    TutorRequestId = tutorRequest.TutorRequestId,
                    BookedTime = date,
                    BookingStatus = Session.BookingStatuses.AwaitingInitialPayment.ToString(),
                };
                if (tutorRequest.OnlineSession)
                {
                    newSession.OnlineSession = new OnlineSession()
                    {
                        SessionOrder = new SessionOrder()
                        {
                            OrderStatus = SessionOrder.OrderStatuses.AwaitingPayment.ToString()
                        }
                    };
                }
                sessions.Add(newSession);
            }
            return sessions;
        }

        public PaymentLink GenerateLinkForBooking(Guid tutorRequestId, Student student, CreatePaymentLinkDTO paymentLinkDTO)
        {
            var tutorRequests = _tutorRequestRepo.GetTutorRequestsForStudent(student.StudentId);
            var tutorRequest = tutorRequests.FirstOrDefault(e => e.TutorRequestId == tutorRequestId);
            if (tutorRequest == null)
            {
                throw new RequestUnauthorizedException("User is not authorized to request resource");
            }
            var allSessions = tutorRequest.Sessions;

            var sortedSessions = allSessions.OrderByDescending(e => e.BookedTime);
            if (sortedSessions.First().BookingStatus != Session.BookingStatuses.AwaitingInitialPayment.ToString())
            {
                throw new BadRequestException("Sessions have already been booked");
            }
            var checkoutOptions = GetSessionCheckoutOptions(sortedSessions.First(), paymentLinkDTO);
            var checkoutItem = GetSessionCheckoutItem(sortedSessions.First());
            var paymentLink = _paymentService.GeneratePaymentLink(paymentLinkDTO, checkoutOptions, checkoutItem);
            return paymentLink;
        }

        public CheckoutOptions GetSessionCheckoutOptions(Session session, CreatePaymentLinkDTO paymentLinkDTO)
        {
            User userToBePaid = session.TutorRequest.TaughtSubject.Tutor.User;
            if (userToBePaid.PaymentAccountDetail == null || userToBePaid.PaymentAccountDetail.YenePaySellerCode == null)
            {
                throw new BadRequestException("Tutor Hasn't setup payment details.");
            }
            var checkoutOptions = new CheckoutOptions()
            {
                CancelReturn = paymentLinkDTO.OnCancelReturn,
                SuccessReturn = paymentLinkDTO.OnSuccessReturn,
                ExpiresAfter = 300000,
                SellerCode = userToBePaid.PaymentAccountDetail.YenePaySellerCode,
                UseSandbox = Environment.GetEnvironmentVariable("ENVIRONMENT") == null
            };
            return checkoutOptions;
        }

        public CheckoutItem GetSessionCheckoutItem(Session session)
        {
            if (session.OnlineSession == null)
            {
                throw new BadRequestException("Session can't be paid for");
            }
            var tutor = session.TutorRequest.TaughtSubject.Tutor.User;
            var checkoutItem = new CheckoutItem()
            {
                ItemId = session.OnlineSession.SessionOrder.SessionOrderId.ToString(),
                ItemName = $"Tutoring Session with {tutor.FirstName} {tutor.LastName}",
                UnitPrice = Convert.ToDecimal(session.GetSessionCost())
            };
            return checkoutItem;
        }
    }
}