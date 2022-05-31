using elearning_platform.Data;
using elearning_platform.Models;
using Microsoft.EntityFrameworkCore;

namespace elearning_platform.Repo
{
    public class TutorRequestRepo : ITutorRequestRepo
    {
        private readonly AppDbContext _ctx;

        public TutorRequestRepo(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public TutorRequest CreateTutorRequest(TutorRequest tutorRequest)
        {
            _ctx.TutorRequests.Add(tutorRequest);
            _ctx.SaveChanges();
            return tutorRequest;
        }

        public TutorRequest? GetTutorRequestById(Guid id)
        {
            return _ctx.TutorRequests.FirstOrDefault(request => request.TutorRequestId == id);
        }

        public IEnumerable<TutorRequest> GetTutorRequestsForStudent(Guid uid)
        {
            return _ctx.TutorRequests.Where(e => e.StudentId == uid);
        }

        public IEnumerable<TutorRequest> GetTutorRequestsForTutor(Guid uid)
        {
            return _ctx.TutorRequests.Include(e => e.TaughtSubject).Where(e => e.TaughtSubject.TutorId == uid).ToList();
        }

        public IEnumerable<TutorRequest> GetTutorRequestsForUser(Guid id)
        {
            var requests = _ctx.TutorRequests.Include(e => e.TaughtSubject).Include(e => e.Student).Include(e => e.Sessions);
            return requests.Where(e => e.TaughtSubject.Tutor.Uid == id || e.Student.Uid == id);
        }

        public TutorRequest? UpdateRequest(TutorRequest request)
        {
            var entity = _ctx.TutorRequests.FirstOrDefault(e => e.TutorRequestId == request.TutorRequestId);
            if (entity == null)
            {
                return null;
            }
            _ctx.ChangeTracker.DetectChanges();
            _ctx.Entry(entity).CurrentValues.SetValues(request);
            _ctx.SaveChanges();
            return request;
        }
    }
}