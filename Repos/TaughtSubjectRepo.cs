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
    }
}