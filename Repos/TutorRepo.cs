using elearning_platform.Data;
using elearning_platform.Models;
using Microsoft.EntityFrameworkCore;
namespace elearning_platform.Repo
{
    public class TutorRepo : ITutorRepo
    {
        private readonly AppDbContext _ctx;

        public TutorRepo(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public Tutor? GetTutorById(Guid tutorId)
        {
            return _ctx.Tutors.FirstOrDefault(tutor => tutor.TutorId == tutorId);
        }

        public Tutor? GetTutorByUid(Guid uid)
        {
            return _ctx.Tutors.FirstOrDefault(tutor => tutor.Uid == uid);
        }

        public IEnumerable<Tutor> GetTutorsForStudent(Guid uid)
        {
            return _ctx.TutorRequests.Include(e => e.TaughtSubject.Tutor).Select(e => e.TaughtSubject.Tutor).Distinct();
        }

        public Tutor CreateTutor(Tutor tutor)
        {
            _ctx.Tutors.Add(tutor);
            _ctx.SaveChanges();
            return tutor;
        }
    }
}