using elearning_platform.Models;

namespace elearning_platform.Repo
{
    public interface ISubjectRepo
    {
        Subject CreateSubject(Subject subject);

        Subject? GetSubjectById(Guid subjectId);
    }
}