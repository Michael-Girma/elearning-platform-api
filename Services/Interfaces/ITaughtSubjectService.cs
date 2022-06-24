using elearning_platform.DTO;
using elearning_platform.Models;

namespace elearning_platform.Services
{
    public interface ITaughtSubjectService
    {
        TaughtSubject CreateTaughtSubject(User user, CreateTaughtSubjectDTO taughtSubjectDTO);
        IEnumerable<TaughtSubject> GetTaughtSubjectBySid(Guid subjectId);

        IEnumerable<TaughtSubject> GetTaughtSubjectsForTutor(Guid tutorId);
        TaughtSubject UpdateTaughtSubject(Guid tutorId, Guid taughtSubjectId, UpdateTaughtSubjectDTO taughtSubjectDTO);
        IEnumerable<TaughtSubject> GetAllTaughtSubjects();
        TaughtSubject ApproveLesson(Guid taughtSubjectId, bool approval);

        bool DeleteTaughtSubjectForTutor(Guid tutorId, Guid taughtSubjectId);
    }
}