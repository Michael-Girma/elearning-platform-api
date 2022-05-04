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
            return _ctx.TutorRequests.Where(e => e.StudentId == uid).ToList();
        }

        public IEnumerable<TutorRequest> GetTutorRequestsForTutor(Guid uid)
        {
            return _ctx.TutorRequests.Include(e => e.TaughtSubject).Where(e => e.TaughtSubject.TutorId == uid).ToList();
        }

        public IEnumerable<TutorRequest> GetTutorRequestsForUser(Guid id)
        {
            var requests = _ctx.TutorRequests.Include(e => e.TaughtSubject);
            return requests.Where(e => e.TaughtSubject.TutorId == id || e.StudentId == id).ToList();
        }
    }
}