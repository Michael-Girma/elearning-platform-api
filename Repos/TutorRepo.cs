using elearning_platform.Data;
using elearning_platform.Models;

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

        public Tutor CreateTutor(Tutor tutor)
        {
            _ctx.Tutors.Add(tutor);
            _ctx.SaveChanges();
            return tutor;
        }
    }
}