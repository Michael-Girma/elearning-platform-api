using AutoMapper;
using elearning_platform.DTO;
using elearning_platform.Repo;

namespace elearning_platform.Services
{
    public class StatService : IStatService
    {
        private readonly StudentRepo _studentRepo;
        private readonly TutorRequestRepo _tutorRequestRepo;
        private readonly SessionRepo _sessionRepo;
        private readonly IMapper _mapper;

        public StatService(StudentRepo studentRepo, TutorRequestRepo tutorRequestRepo, SessionRepo sessionRepo, IMapper mapper)
        {
            _studentRepo = studentRepo;
            _tutorRequestRepo = tutorRequestRepo;
            _sessionRepo = sessionRepo;
            _mapper = mapper;
        }
        public ReadStudentActivityDTO GetStudentActivity(Guid id)
        {
            var student = _studentRepo.GetStudentById(id);
            var tutorRequests = _tutorRequestRepo.GetTutorRequestsForStudent(id);
            var sessions = _sessionRepo.GetSessionsForStudent(id).ToList();
            var activity = new ReadStudentActivityDTO(){
                TutorRequests = _mapper.Map<List<ReadTutorRequestDTO>>(tutorRequests),
                Sessions = _mapper.Map<List<ReadSessionDTO>>(sessions)
            };
            return activity;
        }
    }
}