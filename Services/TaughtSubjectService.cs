using AutoMapper;
using elearning_platform.Data;
using elearning_platform.DTO;
using elearning_platform.Exceptions;
using elearning_platform.Models;
using elearning_platform.Repo;

namespace elearning_platform.Services
{
    public class TaughtSubjectService : ITaughtSubjectService
    {
        private readonly ITaughtSubjectRepo _taughtSubjectRepo;
        private readonly ISubjectRepo _subjectRepo;
        private readonly ITutorRepo _tutorRepo;
        private readonly IFileRepo _fileRepo;
        private readonly IMapper _mapper;

        public TaughtSubjectService(IFileRepo fileRepo, AppDbContext ctx, ITaughtSubjectRepo taughtSubjectRepo, IMapper mapper, ITutorRepo tutorRepo, ISubjectRepo subjectRepo)
        {
            _taughtSubjectRepo = taughtSubjectRepo;
            _subjectRepo = subjectRepo;
            _mapper = mapper;
            _tutorRepo = tutorRepo;
            _fileRepo = fileRepo;
        }

        public TaughtSubject ApproveLesson(Guid taughtSubjectId, bool approval)
        {
            var lesson = _taughtSubjectRepo.GetTaughtSubjectById(taughtSubjectId);
            lesson.Approved = approval;
            _taughtSubjectRepo.UpdateTaughtSubject(lesson);
            return lesson;
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
            taughtSubjectModel.Subject = _subjectRepo.GetSubjectById(taughtSubjectModel.SubjectId);
            return taughtSubjectModel;
        }

        public bool DeleteTaughtSubjectForTutor(Guid tutorId, Guid taughtSubjectId)
        {
            var taughtSubject = _taughtSubjectRepo.GetTaughtSubjectForTutor(tutorId).FirstOrDefault(e => e.TaughtSubjectId == taughtSubjectId);
            if(taughtSubject == null)
            {
                throw new BadRequestException("Lesson doesn't exist");
            }
            var upcomingSessions = _taughtSubjectRepo.GetSessionsForTaughtSubject(taughtSubjectId).Any(e => e.BookedTime > DateTime.UtcNow);
            if(upcomingSessions)
            {
                throw new BadRequestException("Lesson has upcoming sessions");
            }
            return _taughtSubjectRepo.DeleteTaughtSubject(taughtSubjectId);
        }

        public IEnumerable<TaughtSubject> GetAllTaughtSubjects()
        {
            return _taughtSubjectRepo.GetAllTaughtSubjects().Where(e => !e.Deleted);
        }

        public IEnumerable<TaughtSubject> GetTaughtSubjectBySid(Guid subjectId)
        {
            return _taughtSubjectRepo.GetTaughtSubjectBySid(subjectId).Where(e => !e.Deleted);
        }

        public IEnumerable<TaughtSubject> GetTaughtSubjectsForTutor(Guid tutorId)
        {
            return _taughtSubjectRepo.GetTaughtSubjectForTutor(tutorId);
        }

        public TaughtSubject UpdateTaughtSubject(Guid tutorId, Guid taughtSubjectId, UpdateTaughtSubjectDTO taughtSubjectDTO)
        {
            var taughtSubject = _taughtSubjectRepo.GetTaughtSubjectForTutor(tutorId).FirstOrDefault(e => e.TaughtSubjectId == taughtSubjectId);
            if(taughtSubject == null){
                throw new BadRequestException("Tutor does not have lesson with this Id");
            }
            var upcomingSessions = _taughtSubjectRepo.GetSessionsForTaughtSubject(taughtSubjectId).Any(e => e.BookedTime > DateTime.UtcNow);
            if(upcomingSessions)
            {
                throw new BadRequestException("Lesson has upcoming sessions");
            }
            var editedTaughtSubject = _mapper.Map(taughtSubjectDTO, taughtSubject);
            
            _taughtSubjectRepo.UpdateTaughtSubject(editedTaughtSubject);
            foreach(var document in editedTaughtSubject.LessonDocuments)
            {
                document.InternalFileMetadata = _fileRepo.GetInternalFileMetadata(document.InternalFileMetadataId);
            }
            return editedTaughtSubject;
        }

    }
}