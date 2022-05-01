using AutoMapper;
using elearning_platform.DTO;
using elearning_platform.Models;
using elearning_platform.Repo;

namespace elearning_platform.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepo _subjectRepo;
        private readonly IMapper _mapper;

        public SubjectService(ISubjectRepo subjectRepo, IMapper mapper)
        {
            _subjectRepo = subjectRepo;
            _mapper = mapper;
        }
        public Subject CreateSubject(User user, CreateSubjectDTO createSubjectDTO)
        {
            var subjectModel = _mapper.Map<Subject>(createSubjectDTO);
            subjectModel.CreatedBy = user.Uid;
            _subjectRepo.CreateSubject(subjectModel);
            return subjectModel;
        }
    }
}