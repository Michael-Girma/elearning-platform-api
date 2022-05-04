using elearning_platform.Models;
using elearning_platform.DTO;

namespace elearning_platform.Repo
{
    public interface ITaughtSubjectRepo
    {
        TaughtSubject CreateTaughtSubject(TaughtSubject taughtSubject);

        TaughtSubject? GetTaughtSubjectById(Guid id);

        IEnumerable<TaughtSubject> GetTaughtSubjectBySid(Guid subjectId);
    }
}