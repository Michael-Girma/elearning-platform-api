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
            if (taughtSubject == null)
            {
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
    }
}