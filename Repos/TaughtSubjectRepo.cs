using AutoMapper;
using elearning_platform.Data;
using elearning_platform.DTO;
using elearning_platform.Exceptions;
using elearning_platform.Models;

namespace elearning_platform.Repo
{
    public class TaughtSubjectRepo : ITaughtSubjectRepo
    {
        private readonly AppDbContext _ctx;

        public TaughtSubjectRepo(AppDbContext ctx)
        {
            _ctx = ctx;
        }
        public TaughtSubject CreateTaughtSubject(TaughtSubject taughtSubject)
        {
            _ctx.TaughtSubjects.Add(taughtSubject);
            _ctx.SaveChanges();
            return taughtSubject;
        }

        public TaughtSubject? GetTaughtSubjectById(Guid id)
        {
            return _ctx.TaughtSubjects.FirstOrDefault(e => e.TaughtSubjectId == id);
        }

        public IEnumerable<TaughtSubject> GetTaughtSubjectBySid(Guid subjectId)
        {
            return _ctx.TaughtSubjects.Where(e => e.SubjectId == subjectId).ToList();
        }
    }
}