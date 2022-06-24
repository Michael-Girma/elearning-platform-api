using elearning_platform.Data;
using elearning_platform.Models;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using elearning_platform.Exceptions;

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

        public IEnumerable<Session> GetSessionsForTutor(Guid id)
        {
            return _ctx.Sessions.Include(e => e.TutorRequest.TaughtSubject).Where(e => e.TutorRequest.TaughtSubject.TutorId == id);
        }

        public IEnumerable<Session> GetSessionsForUser(Guid id)
        {
            return _ctx.Sessions.Include(e => e.TutorRequest.Student).Include(e => e.TutorRequest.TaughtSubject.Tutor).Where(e => e.TutorRequest.Student.Uid == id || e.TutorRequest.TaughtSubject.Tutor.Uid == id).Distinct().Include(e => e.TutorRequest.TaughtSubject.Tutor);
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

        public SessionFeedback SaveFeedback(SessionFeedback feedback)
        {
            _ctx.SessionFeedbacks.Add(feedback);
            _ctx.SaveChanges();
            return feedback;
        }

        public IEnumerable<SessionFeedback> GetFeedbackOfStudent(Guid studentId)
        {
            var feedbacks = _ctx.SessionFeedbacks.Where(feedback => feedback.Session.TutorRequest.StudentId == studentId);
            return feedbacks;
        }

        public IEnumerable<SessionFeedback> GetFeedbackForTutor(Guid tutorId)
        {
            var feedbacks = _ctx.SessionFeedbacks.Where(feedback => feedback.Session.TutorRequest.TaughtSubject.TutorId == tutorId);
            return feedbacks;
        }

        public Resource AddResource(Resource resource)
        {
            _ctx.Resources.Add(resource);
            _ctx.SaveChanges();
            return resource;
        }

        public bool DeleteResource(Guid resourceId)
        {
            var resource = _ctx.Resources.FirstOrDefault(e => e.ResourceId == resourceId);
            if(resource == null)
            {
                throw new BadRequestException("");
            }
            _ctx.Resources.Remove(resource);
            return _ctx.SaveChanges() > -1;
        }

        public Assessment SaveAssessment(Assessment assessment)
        {
            _ctx.Assessments.Add(assessment);
            _ctx.SaveChanges();
            return assessment;
        }

        public bool DeleteAssessment(Guid assessmentId)
        {
            var assessment = _ctx.Assessments.FirstOrDefault(e => e.AssessmentId == assessmentId);
            if(assessment == null)
            {
                throw new BadRequestException("");
            }
            _ctx.Assessments.Remove(assessment);
            return _ctx.SaveChanges() > -1;
        }

        public IEnumerable<Session> GetSessions()
        {
            return _ctx.Sessions.ToList();
        }

        public IEnumerable<SessionFeedback> GetAllFeedbacks()
        {
            return _ctx.SessionFeedbacks.ToList();
        }

        public SessionFeedback UpdateSessionFeedback(SessionFeedback feedback)
        {
            var entity = _ctx.SessionFeedbacks.FirstOrDefault(e => e.Id == feedback.Id);
            if (entity == null)
            {
                return null;
            }
            _ctx.Entry(entity).CurrentValues.SetValues(feedback);
            _ctx.SaveChanges();
            return entity;   
        }
    }
}