using elearning_platform.Data;
using elearning_platform.Models;

namespace elearning_platform.Repo
{
    public class SubjectRepo : ISubjectRepo
    {
        private readonly AppDbContext _ctx;

        public SubjectRepo(AppDbContext ctx)
        {
            _ctx = ctx;
        }
        public Subject CreateSubject(Subject newSubject)
        {
            var existingSubject = _ctx.Subjects.FirstOrDefault(subject => subject.EducationLevelId == newSubject.EducationLevelId && subject.Name == newSubject.Name);
            if (existingSubject != null)
            {
                return existingSubject;
            }
            else
            {
                _ctx.Subjects.Add(newSubject);
                _ctx.SaveChanges();
                return newSubject;
            }
        }

        public Subject? GetSubjectById(Guid subjectId)
        {
            return _ctx.Subjects.FirstOrDefault(subject => subject.SubjectId == subjectId);
        }
    }
}