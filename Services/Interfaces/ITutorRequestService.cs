using elearning_platform.Models;

namespace elearning_platform.Services
{
    public interface ITutorRequestService
    {
        IEnumerable<TutorRequest> GetRequestsForStudent(Guid id);
        IEnumerable<TutorRequest> GetRequestsForTutor(Guid id);
    }
}