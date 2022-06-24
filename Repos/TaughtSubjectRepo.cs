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

        public bool DeleteTaughtSubject(Guid taughtSubjectId)
        {
            var taughtSubject = _ctx.TaughtSubjects.FirstOrDefault(e => e.TaughtSubjectId == taughtSubjectId);
            if(taughtSubject == null)
            {
                throw new BadRequestException("");
            }
            // _ctx.TaughtSubjects.Remove(taughtSubject);
            taughtSubject.Deleted = true;
            return _ctx.SaveChanges() > -1;
        }

        public IEnumerable<TaughtSubject> GetAllTaughtSubjects()
        {
            return _ctx.TaughtSubjects.ToList();
        }

        public IEnumerable<Session> GetSessionsForTaughtSubject(Guid taughtSubjectId)
        {
            return _ctx.Sessions.Where(e => e.TutorRequest.TaughtSubjectId == taughtSubjectId);
        }

        public TaughtSubject? GetTaughtSubjectById(Guid id)
        {
            return _ctx.TaughtSubjects.FirstOrDefault(e => e.TaughtSubjectId == id);
        }

        public IEnumerable<TaughtSubject> GetTaughtSubjectBySid(Guid subjectId)
        {
            return _ctx.TaughtSubjects.Where(e => e.SubjectId == subjectId).ToList();
        }

        public IEnumerable<TaughtSubject> GetTaughtSubjectForTutor(Guid tutorId)
        {
            return _ctx.TaughtSubjects.Where(e => e.TutorId == tutorId);
        }

        public TaughtSubject UpdateTaughtSubject(TaughtSubject ts)
        {
            var entity = _ctx.TaughtSubjects.FirstOrDefault(e => e.TaughtSubjectId == ts.TaughtSubjectId);
            if (entity == null)
            {
                return null;
            }
            _ctx.ChangeTracker.DetectChanges();
            // _ctx.TaughtSubjects.Remove(taughtSubject);
            var docs = _ctx.LessonDocuments.Where(e => e.TaughtSubjectId == ts.TaughtSubjectId);
            _ctx.LessonDocuments.RemoveRange(docs);
            _ctx.Entry(entity).CurrentValues.SetValues(ts);
            _ctx.SaveChanges();
            return entity;
        }
    }
}