using elearning_platform.DTO;
using elearning_platform.Models;

namespace elearning_platform.Services
{
    public interface ISubjectService
    {
        Subject CreateSubject(User user, CreateSubjectDTO createSubjectDTO);
        Subject EditSubject(Guid subjectId, CreateSubjectDTO createSubjectDTO);

        StarredSubject StarSubject(User user, Guid subjectId);
        bool UnstarSubject(User user, Guid subjectId);

        IEnumerable<StarredSubject> GetStarredSubjectsForUser(User user);

        IEnumerable<Subject> GetAllSubjects();
        IEnumerable<SubjectRequest> GetAllSubjectRequests();

        Subject? GetSubjectById(Guid subjectId);

        IEnumerable<EducationLevel> GetEducationLevels();
        EducationLevel AddEducationLevel(string level);

        SubjectRequest RequestSubject(Tutor tutor, CreateSubjectRequestDTO dto);
        SubjectRequest AddressSubjectRequest(Guid requestId, bool approval);
    }
}