using AutoMapper;
using elearning_platform.DTO;
using elearning_platform.Repo;

namespace elearning_platform.Services
{
    public class StatService : IStatService
    {
        private readonly IStudentRepo _studentRepo;
        private readonly ITutorRequestRepo _tutorRequestRepo;
        private readonly ISessionRepo _sessionRepo;
        private readonly ITutorRepo _tutorRepo;
        private readonly IMapper _mapper;
        private readonly IUserRepo _userRepo;
        private readonly ISubjectRepo _subjectRepo;
        private readonly IPaymentRepo _paymentRepo;

        public StatService(IPaymentRepo paymentRepo, ISubjectRepo subjectRepo, IUserRepo userRepo, IStudentRepo studentRepo, ITutorRepo tutorRepo, ITutorRequestRepo tutorRequestRepo, ISessionRepo sessionRepo, IMapper mapper)
        {
            _studentRepo = studentRepo;
            _tutorRequestRepo = tutorRequestRepo;
            _sessionRepo = sessionRepo;
            _mapper = mapper;
            _tutorRepo = tutorRepo;
            _userRepo = userRepo;
            _subjectRepo = subjectRepo;
            _paymentRepo = paymentRepo;
        }

        public ReadPlatformOverviewDTO GetPlatformOverview()
        {
            // var logins = _userRepo.getDailyUsers()
            var logins = 5;
            var dailyUsers = _userRepo.GetUsers().Where(e => e.CreatedOn > DateTime.Today.AddDays(-1)).ToList();
            var weeklyUsers = _userRepo.GetUsers().Where(e => e.CreatedOn > DateTime.Today.AddDays(-7)).ToList();
            var weeklySessions = _sessionRepo.GetSessions().Where(e => e.CreatedOn > DateTime.Today.AddDays(-7)).ToList();
            var revenue = _paymentRepo.GetAllPaymentDetails().Where(e => e.Status == "Paid").Select(e => e.TotalAmount).Sum();
            var unaddressedReports = _sessionRepo.GetAllFeedbacks().Where(e => e.Report && !e.ReportAddressed).ToList();
            var weeklyRevenue = _paymentRepo.GetAllPaymentDetails().Where(e => e.Status == "Paid" && e.UpdatedOn > DateTime.Today.AddDays(-7)).Select(e => e.TotalAmount).Sum();
            var weeklyOnlineSessions = (from session in weeklySessions select session.OnlineSession).Where(e => e != null && e.OnlineSessionId != null).ToList();
            var subjects = _subjectRepo.GetAllSubjects().ToList();
            var subjectRequests = _subjectRepo.GetAllSubjectRequests().ToList();

            return new ReadPlatformOverviewDTO(){
                DailyUsers = _mapper.Map<IEnumerable<ReadUserDTO>>(dailyUsers),
                WeeklyUsers =  _mapper.Map<IEnumerable<ReadUserDTO>>(weeklyUsers),
                WeeklySessions = _mapper.Map<IEnumerable<ReadSessionDTO>>(weeklySessions),
                WeeklyOnlineSessions = _mapper.Map<IEnumerable<ReadOnlineSessionDTO>>(weeklyOnlineSessions),
                Revenue = revenue,
                WeeklyRevenue = weeklyRevenue,
                UnaddressedReports = _mapper.Map<IEnumerable<ReadSessionFeedbackDTO>>(unaddressedReports),
                Subjects = _mapper.Map<IEnumerable<ReadSubjectDTO>>(subjects),
                SubjectRequests = _mapper.Map<IEnumerable<ReadSubjectRequestDTO>>(subjectRequests)
            };
        }

        public ReadStudentActivityDTO GetStudentActivity(Guid id)
        {
            var student = _studentRepo.GetStudentById(id);
            var tutorRequests = _tutorRequestRepo.GetTutorRequestsForStudent(id).ToList();
            var sessions = _sessionRepo.GetSessionsForStudent(id).ToList();
            var activity = new ReadStudentActivityDTO(){
                TutorRequests = _mapper.Map<List<ReadTutorRequestDTO>>(tutorRequests),
                Sessions = _mapper.Map<List<ReadSessionDTO>>(sessions)
            };
            return activity;
        }

        public ReadTutorStatsDTO GetTutorActivity(Guid id)
        {
            var tutor = _tutorRepo.GetTutorById(id);
            var sessions = _sessionRepo.GetSessionsForTutor(id);
            var requests = _tutorRequestRepo.GetTutorRequestsForTutor(id);
            var revenue = from session in sessions select session.GetSessionCost();
            var stats = new ReadTutorStatsDTO(){
                Feedbacks = _mapper.Map<IEnumerable<ReadSessionFeedbackDTO>>(tutor.Feedbacks.ToList()),
                Sessions = _mapper.Map<IEnumerable<ReadSessionDTO>>(sessions.ToList()),
                TutorRequests = _mapper.Map<IEnumerable<ReadTutorRequestDTO>>(requests.ToList()),
                Revenue = revenue.Sum()
            };
            return stats;
        }
    }
}