using elearning_platform.Models;

namespace elearning_platform.Repo
{
    public interface ITutorRequestRepo
    {
        TutorRequest? GetTutorRequestById(Guid id);
        IEnumerable<TutorRequest> GetTutorRequestsForStudent(Guid uid);
        IEnumerable<TutorRequest> GetTutorRequestsForTutor(Guid uid);
        IEnumerable<TutorRequest> GetTutorRequestsForUser(Guid uid);

        TutorRequest CreateTutorRequest(TutorRequest request);

        TutorRequest? UpdateRequest(TutorRequest request);
    }
}