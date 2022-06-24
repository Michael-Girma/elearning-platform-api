using elearning_platform.Models;

namespace elearning_platform.Repo
{
    public interface ISubjectRepo
    {
        Subject CreateSubject(Subject subject);

        Subject? GetSubjectById(Guid subjectId);

        StarredSubject StarSubject(StarredSubject starredSubject);
        bool RemoveStarredSubject(StarredSubject starredSubject);

        IEnumerable<StarredSubject> GetStarredSubjectsForUser(User user);

        IEnumerable<Subject> GetAllSubjects();

        IEnumerable<EducationLevel> GetEducationLevels();

        SubjectRequest CreateSubjectRequest(SubjectRequest request);
        EducationLevel AddEducationLevel(EducationLevel level);
        IEnumerable<SubjectRequest> GetAllSubjectRequests();
        SubjectRequest SaveSubjectRequest(SubjectRequest request);
        Subject SaveSubject(Subject subject);

    }
}