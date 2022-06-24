using AutoMapper;
using elearning_platform.DTO;
using elearning_platform.Exceptions;
using elearning_platform.ExtensionMethods;
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
        private readonly ITutorService _tutorService;
        private readonly IPaymentRepo _paymentRepo;
        private readonly ITutorRepo _tutorRepo;
        private readonly IStudentRepo _studentRepo;
        private readonly ISessionRepo _sessionRepo;
        private readonly IMapper _mapper;

        public SessionService(ITutorRequestRepo tutorRequestRepo, ITutorRepo tutorRepo, IStudentRepo studentRepo, ITutorService tutorService, IMapper mapper, ITaughtSubjectRepo taughtSubjectRepo, IPaymentRepo paymentRepo, IPaymentService paymentService, ISessionRepo sessionRepo)
        {
            _tutorRequestRepo = tutorRequestRepo;
            _taughtSubjectRepo = taughtSubjectRepo;
            _paymentService = paymentService;
            _tutorService = tutorService;
            _tutorRepo = tutorRepo;
            _studentRepo = studentRepo;
            _sessionRepo = sessionRepo;
            _paymentRepo = paymentRepo;
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
                    SessionLength = tutorRequest.SessionLength,
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

        public SessionPaymentLink GenerateLinkForBooking(Guid tutorRequestId, Student student, CreatePaymentLinkDTO paymentLinkDTO)
        {
            var tutorRequest = _tutorRequestRepo.GetTutorRequestsForStudent(student.StudentId).FirstOrDefault(e => e.TutorRequestId == tutorRequestId);
            if (tutorRequest == null)
            {
                throw new RequestUnauthorizedException("User is not authorized to request resource");
            }
            if(tutorRequest.Sessions.Count <= 0)
            {
                throw new BadRequestException("Student doesn't have upcoming sessions");
            }

            var sortedSessions = tutorRequest.Sessions.Where(e => e.OnlineSession != null).OrderByDescending(e => e.BookedTime);
            var firstSession = sortedSessions.First();
            if (firstSession.BookingStatus != Session.BookingStatuses.AwaitingInitialPayment.ToString())
            {
                throw new BadRequestException("Sessions have already been booked");
            }
            // var checkoutOptions = GetSessionCheckoutOptions(sortedSessions.First(), paymentLinkDTO);
            // var checkoutItem = GetSessionCheckoutItem(sortedSessions.First());
            // var paymentLink = _mapper.Map<SessionPaymentLink>(paymentLinkDTO);
            // paymentLink.OrderId = firstSession.OnlineSession.SessionOrder.SessionOrderId;
            // paymentLink = _paymentService.GenerateSessionPaymentLink(paymentLink, checkoutOptions, checkoutItem);
            // return paymentLink;
            return GenerateLinkForSession(firstSession.SessionId, student, paymentLinkDTO);
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
                IpnUrlReturn = paymentLinkDTO.OnSuccessReturn,
                ExpiresAfter = 300000,
                OrderId = session.OnlineSession.SessionOrder.SessionOrderId.ToString(),
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


        public async Task<Session> BookSession(PaymentDetail iPNModel)
        {
            var isAuthenticIPN = await _paymentService.VerifyPaymentDetail(iPNModel);
            if(isAuthenticIPN)
            {
                throw new RequestUnauthorizedException("Supplied payment detail is not authentic");
            }
            var sessionOrder = _sessionRepo.GetSessionOrderById(Guid.Parse(iPNModel.MerchantOrderId));
            if(sessionOrder == null)
            {
                throw new Exception();
            }
            _paymentRepo.SavePaymentDetail(iPNModel);
            var session = sessionOrder.OnlineSession.Session;
            session.OnlineSession.VideoChatLink = GenerateVideoChatLink(session.SessionId);
            session.OnlineSession.SessionOrder.OrderStatus = SessionOrder.OrderStatuses.Paid.ToString();
            session.BookingStatus = Session.BookingStatuses.Booked.ToString();
            _sessionRepo.UpdateSession(session);
            return session;
        }

        public async Task<ReadEnquiryInsightDTO?> GetEnquiryInsights(Guid tutorRequestId)
        {
            var tutorRequest = _tutorRequestRepo.GetTutorRequestById(tutorRequestId);
            if(tutorRequest == null){
                throw new BadRequestException("Enquiry doesn't exist");
            }
            var tutorDetails = _tutorService.GetTutorDetails(tutorRequest.TaughtSubject.TutorId);
            return new ReadEnquiryInsightDTO(){
                Id = tutorRequestId,
                TaughtSubject = _mapper.Map<ReadTaughtSubjectDTO>(tutorRequest.TaughtSubject),
                TutorSessionCount = tutorDetails.Sessions.Count(),
                TutorTeachesOnline = tutorDetails.TeachesOnline,
                Verified = tutorDetails.Verified
            };
        }

        public async Task<IEnumerable<ReadEnquiryInsightDTO>> GetAllEnquiryInsights(Guid studentId)
        {
            var student = _studentRepo.GetStudentById(studentId);
            var tutors = _tutorRepo.GetTutorsForStudent(studentId).ToList();
            List<ReadEnquiryInsightDTO> myList=new List<ReadEnquiryInsightDTO>();
            var requests = from tutor in tutors select student.TutorRequests.FirstOrDefault(e => e.TaughtSubject.TutorId == tutor.TutorId);
            foreach(var request in requests.ToList())
            {
                myList.Add(await GetEnquiryInsights(request.TutorRequestId));
            }
            return myList;
        }

        public IEnumerable<Session> GetAllSessionsForUser(Guid id)
        {
            var sessions = _sessionRepo.GetSessionsForUser(id);
            foreach(var session in sessions)
            {
                var tutor  = session.TutorRequest.TaughtSubject.Tutor;

            }
            var sessionList = sessions.ToList();
            return sessionList;
        }

        public SessionPaymentLink GenerateLinkForSession(Guid sessionId, Student student, CreatePaymentLinkDTO paymentLinkDTO)
        {
            var session = _sessionRepo.GetSessionById(sessionId);
            if(session == null)
            {
                throw new BadRequestException("Session doesn't exist");
            }
            var checkoutOptions = GetSessionCheckoutOptions(session, paymentLinkDTO);
            var checkoutItem = GetSessionCheckoutItem(session);
            var paymentLink = _mapper.Map<SessionPaymentLink>(paymentLinkDTO);
            paymentLink.OrderId = session.OnlineSession.SessionOrder.SessionOrderId;
            paymentLink = _paymentService.GenerateSessionPaymentLink(paymentLink, checkoutOptions, checkoutItem);
            return paymentLink;
        }

        public string GenerateVideoChatLink(Guid sessionId)
        {
            return "/etutorapp/" + Guid.NewGuid().ToString();
        }

        public SessionFeedback LeaveFeedback(Student student, Guid sessionId, CreateSessionFeedbackDTO feedbackDTO)
        {
            var session = _sessionRepo.GetSessionsForStudent(student.StudentId).FirstOrDefault(session => session.SessionId == sessionId);
            if(session == null || session.BookedTime > DateTime.Now)
            {
                throw new BadRequestException("Student has not participated in this session");
            }
            var feedback = _mapper.Map<SessionFeedback>(feedbackDTO);
            feedback.SessionId = session.SessionId;
            feedback.StudentId = student.StudentId;
            feedback.TutorId = session.TutorRequest.TaughtSubject.TutorId;
            _sessionRepo.SaveFeedback(feedback);
            return feedback;
        }

        public IEnumerable<SessionFeedback> GetFeedbacksOfStudent(Guid studentId)
        {
            return _sessionRepo.GetFeedbackOfStudent(studentId);
        }

        public IEnumerable<SessionFeedback> GetFeedbacksOfTutor(Guid tutorId)
        {
            return _sessionRepo.GetFeedbackForTutor(tutorId);
        }

        public Session UpdateStudentNotes(Student student, Guid sessionId, UpdateStudentNotesDTO notesDTO)
        {
            var session = _sessionRepo.GetSessionsForStudent(student.StudentId).FirstOrDefault(e => e.SessionId == sessionId);
            if(session == null)
            {
                throw new BadRequestException("Student isn't enrolled in session with such Id");
            }
            session.StudentNotes = notesDTO.StudentNotes;
            _sessionRepo.UpdateSession(session);
            return session;
        }

        public Session UpdateRecommendations(Tutor tutor, Guid sessionId, UpdateRecommendationsDTO recommendationsDTO)
        {
            var session = _sessionRepo.GetSessionsForTutor(tutor.TutorId).FirstOrDefault(e => e.SessionId == sessionId);
            if(session == null)
            {
                throw new BadRequestException("Tutor isn't enrolled in session with such Id");
            }
            session.Recommendations = recommendationsDTO.Recommendations;
            _sessionRepo.UpdateSession(session);
            return session;  
        }

        public Resource UploadResource(Tutor tutor, Guid sessionId, CreateResourceDTO resourceDTO)
        {
            var session = _sessionRepo.GetSessionsForTutor(tutor.TutorId).FirstOrDefault(e => e.SessionId == sessionId);
            if(session == null)
            {
                throw new BadRequestException("Tutor isn't enrolled in session with such Id");
            }
            var resource = _mapper.Map<Resource>(resourceDTO);
            resource.SessionId = sessionId;
            _sessionRepo.AddResource(resource);
            return resource;
        }

        public bool RemoveResource(Tutor tutor, Guid resourceId)
        {
            var session = _sessionRepo.GetSessionsForTutor(tutor.TutorId);
            if(session.Count() == 0)
            {
                throw new BadRequestException("Tutor isn't enrolled in session with such Id");
            }
            _sessionRepo.DeleteResource(resourceId);
            return true;
        }

        public Assessment AddAssessment(Tutor tutor, Guid sessionId, CreateAssessmentDTO assessmentDTO)
        {
            var session = _sessionRepo.GetSessionsForTutor(tutor.TutorId);
            if(session.Count() == 0)
            {
                throw new BadRequestException("Tutor isn't enrolled in session with such Id");
            }
            var assessment = _mapper.Map<Assessment>(assessmentDTO);
            assessment.SessionId = sessionId;
            var newAssessment = _sessionRepo.SaveAssessment(assessment);
            return newAssessment;
        }

        public bool RemoveAssessment(Tutor tutor, Guid assessmentId)
        {
            var session = _sessionRepo.GetSessionsForTutor(tutor.TutorId);
            if(session == null)
            {
                throw new BadRequestException("Tutor isn't enrolled in session with such Id");
            }
            _sessionRepo.DeleteAssessment(assessmentId);
            return true;   
        }

        public IEnumerable<SessionFeedback> GetAllReports()
        {
            return _sessionRepo.GetAllFeedbacks().Where(e => e.Report == true);
        }

        public SessionFeedback MarkAsAddressed(Guid feedbackId)
        {
            var feedback = _sessionRepo.GetAllFeedbacks().FirstOrDefault(e => e.Id == feedbackId);
            feedback.ReportAddressed = true;
            _sessionRepo.UpdateSessionFeedback(feedback);
            return feedback;
        }
    }
}