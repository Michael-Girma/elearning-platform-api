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

        public Subject EditSubject(Guid subjectId, CreateSubjectDTO createSubjectDTO)
        {
            var subject = _subjectRepo.GetSubjectById(subjectId);
            _mapper.Map<CreateSubjectDTO, Subject>(createSubjectDTO, subject);
            
            // _subjectRepo.CreateSubject(subject);
            _subjectRepo.SaveSubject(subject);
            return subject;
        }

        public StarredSubject StarSubject(User user, Guid subjectId)
        {
            var subject = _subjectRepo.GetSubjectById(subjectId);
            if(subject == null)
            {
                throw new BadHttpRequestException("Subject Doesn't exist");
            }
            var starredSubject = new StarredSubject(){
                UserId = user.Uid,
                SubjectId = subjectId
            };
            _subjectRepo.StarSubject(starredSubject);
            return starredSubject;
        }

        public IEnumerable<StarredSubject> GetStarredSubjectsForUser(User user)
        {
            var subjects = _subjectRepo.GetStarredSubjectsForUser(user);
            return subjects;
        }

        public IEnumerable<Subject> GetAllSubjects()
        {
            return _subjectRepo.GetAllSubjects();
        }

        public Subject? GetSubjectById(Guid subjectId)
        {
            return _subjectRepo.GetSubjectById(subjectId);
        }

        public IEnumerable<EducationLevel> GetAllEducationLevels()
        {
            return _subjectRepo.GetEducationLevels();
        }

        public IEnumerable<EducationLevel> GetEducationLevels()
        {
            return _subjectRepo.GetEducationLevels();
        }

        public SubjectRequest RequestSubject(Tutor tutor, CreateSubjectRequestDTO dto)
        {
            var subjectRequest = _mapper.Map<SubjectRequest>(dto);
            subjectRequest.Approved = false;
            subjectRequest.RequestAuthorId = tutor.TutorId;
            return _subjectRepo.CreateSubjectRequest(subjectRequest);
        }

        public IEnumerable<SubjectRequest> GetAllSubjectRequests()
        {
            return _subjectRepo.GetAllSubjectRequests();
        }

        public SubjectRequest AddressSubjectRequest(Guid requestId, bool approval)
        {
            var subjectRequest = _subjectRepo.GetAllSubjectRequests().FirstOrDefault(e => e.SubjectRequestId == requestId);
            if(approval)
            {
                var subject = new Subject(){
                    EducationLevelId = subjectRequest.EducationLevelId,
                    Name = subjectRequest.SubjectName,
                    CreatedBy = subjectRequest.Tutor.Uid
                };
                _subjectRepo.CreateSubject(subject);
                subjectRequest.Approved = true;
                subjectRequest.Addressed = true;
            }else{
                subjectRequest.Approved = false;
                subjectRequest.Addressed = true;
            }
            _subjectRepo.SaveSubjectRequest(subjectRequest);
            return subjectRequest;

        }

        public EducationLevel AddEducationLevel(string level)
        {
            var educationLevel = new EducationLevel(){
                Level = level
            };
            return _subjectRepo.AddEducationLevel(educationLevel);
        }

        public bool UnstarSubject(User user, Guid subjectId)
        {
            var starredSubject = _subjectRepo.GetStarredSubjectsForUser(user).FirstOrDefault(e => e.SubjectId == subjectId);
            return _subjectRepo.RemoveStarredSubject(starredSubject);

        }
    }
}