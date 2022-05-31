using AutoMapper;
using elearning_platform.DTO;
using elearning_platform.Repo;

namespace elearning_platform.Services
{
    public class TutorService : ITutorService
    {
        private readonly ITutorRepo _tutorRepo;
        private readonly ISessionRepo _sessionRepo;
        private readonly IMapper _mapper;

        public TutorService(ITutorRepo tutorRepo, ISessionRepo sessionRepo, IMapper mapper)
        {
            _tutorRepo = tutorRepo;
            _sessionRepo = sessionRepo;
            _mapper = mapper;
        }


        public ReadTutorDetailsDTO GetTutorDetails(Guid tutorId)
        {
            var tutor = _tutorRepo.GetTutorById(tutorId);
            var sessions = _sessionRepo.GetSessionsForTutor(tutorId).ToList();
            return new ReadTutorDetailsDTO(){
                User = _mapper.Map<ReadUserDTO>(tutor.User),
                Sessions = sessions,
                Verified = tutor.Verified,
                TeachesOnline = sessions.Any(e => e.OnlineSession != null)
            };
        }
    }
}