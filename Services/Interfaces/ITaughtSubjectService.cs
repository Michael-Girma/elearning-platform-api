using elearning_platform.DTO;
using elearning_platform.Models;

namespace elearning_platform.Services
{
    public interface ITaughtSubjectService
    {
        TaughtSubject CreateTaughtSubject(User user, CreateTaughtSubjectDTO taughtSubjectDTO);
        IEnumerable<TaughtSubject> GetTaughtSubjectBySid(Guid subjectId);
    }
}