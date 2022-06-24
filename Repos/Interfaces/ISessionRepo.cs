using elearning_platform.Models;

namespace elearning_platform.Repo
{
    public interface ISessionRepo
    {
        Session? GetSessionById(Guid paymentLinkId);

        SessionOrder? GetSessionOrderById(Guid id);

        Session? UpdateSession(Session session);

        IEnumerable<Session> GetSessionsForStudent(Guid id);
        IEnumerable<Session> GetSessionsForTutor(Guid id);

        IEnumerable<Session>  GetSessionsForUser(Guid id);

        SessionFeedback SaveFeedback(SessionFeedback feedback);

        IEnumerable<SessionFeedback> GetFeedbackOfStudent(Guid studentId);
        IEnumerable<SessionFeedback> GetFeedbackForTutor(Guid tutorId);
        IEnumerable<SessionFeedback> GetAllFeedbacks();
        Resource AddResource(Resource resource);
        bool DeleteResource(Guid resourceId);
        bool DeleteAssessment(Guid assessmentId);
        Assessment SaveAssessment(Assessment assessment);

        IEnumerable<Session> GetSessions();

        SessionFeedback UpdateSessionFeedback(SessionFeedback feedback);
        // IEnumerable<SessionFeedback> GetAllReports();
    }
}