using elearning_platform.Data;
using elearning_platform.Models;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace elearning_platform.Repo
{
    public class SessionRepo : ISessionRepo
    {
        private readonly AppDbContext _ctx;

        public SessionRepo(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public Session? GetSessionById(Guid sessionId)
        {
            return _ctx.Sessions.SingleOrDefault(e => e.SessionId == sessionId);
        }

        public SessionOrder? GetSessionOrderById(Guid id)
        {
            return _ctx.SessionOrders.SingleOrDefault(e => e.SessionOrderId == id);
        }

        public IEnumerable<Session> GetSessionsForStudent(Guid id)
        {
            return _ctx.Sessions.Include(e => e.TutorRequest).Where(e => e.TutorRequest.StudentId == id);
        }

        public Session? UpdateSession(Session session)
        {
            var entity = _ctx.Sessions.FirstOrDefault(e => e.SessionId == session.SessionId);
            if (entity == null)
            {
                return null;
            }
            _ctx.Entry(entity).CurrentValues.SetValues(session);
            _ctx.SaveChanges();
            return session;
        }
    }
}