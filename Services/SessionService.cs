using AutoMapper;
using elearning_platform.DTO;
using elearning_platform.Exceptions;
using elearning_platform.Models;
using elearning_platform.Repo;

namespace elearning_platform.Services
{
    public class SessionService : ISessionService
    {
        private readonly ITutorRequestRepo _tutorRequestRepo;
        private readonly ITaughtSubjectRepo _taughtSubjectRepo;
        private readonly IMapper _mapper;

        public SessionService(ITutorRequestRepo tutorRequestRepo, IMapper mapper, ITaughtSubjectRepo taughtSubjectRepo)
        {
            _tutorRequestRepo = tutorRequestRepo;
            _taughtSubjectRepo = taughtSubjectRepo;
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

        public ICollection<Session> CreateSessionsFromTutorRequest(TutorRequest tutorRequest)
        {
            var sessions = new List<Session>();
            foreach (var date in tutorRequest.PreferredDates)
            {
                var newSession = new Session()
                {
                    TutorRequestId = tutorRequest.TutorRequestId,
                    BookedTime = date,
                    BookingStatus = Session.BookingStatuses.AwaitingInitialPayment.ToString(),
                    PaymentStatus = Session.PaymentStatuses.AwaitingPayment.ToString(),
                };
                if (tutorRequest.OnlineSession)
                {
                    newSession.OnlineSession = new OnlineSession()
                    {
                        PaymentOrder = new PaymentOrder()
                        {
                            OrderStatus = PaymentOrder.OrderStatuses.Unpaid.ToString()
                        }
                    };
                }
                sessions.Add(newSession);
            }
            return sessions;
        }
    }
}