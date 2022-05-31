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
    }
}