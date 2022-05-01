using AutoMapper;
using elearning_platform.Data;
using elearning_platform.DTO;
using elearning_platform.Models;
using elearning_platform.Repo;

namespace elearning_platform.Services
{
    public class TaughtSubjectService : ITaughtSubjectService
    {
        private readonly AppDbContext _ctx;
        private readonly ITaughtSubjectRepo _taughtSubjectRepo;
        private readonly IMapper _mapper;
        private readonly ITutorRepo _tutorRepo;

        public TaughtSubjectService(AppDbContext ctx, ITaughtSubjectRepo taughtSubjectRepo, IMapper mapper, ITutorRepo tutorRepo)
        {
            _ctx = ctx;
            _taughtSubjectRepo = taughtSubjectRepo;
            _mapper = mapper;
            _tutorRepo = tutorRepo;
        }

        public TaughtSubject CreateTaughtSubject(User user, CreateTaughtSubjectDTO taughtSubjectDTO)
        {
            var tutor = _tutorRepo.GetTutorByUid(user.Uid);
            if (tutor == null)
            {
                throw new Exception("Tutor Doesn't exist");
            }
            var taughtSubjectModel = _mapper.Map<TaughtSubject>(taughtSubjectDTO);
            taughtSubjectModel.TutorId = tutor.TutorId;
            _taughtSubjectRepo.CreateTaughtSubject(taughtSubjectModel);
            return taughtSubjectModel;
        }
    }
}