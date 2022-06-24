using elearning_platform.Data;
using elearning_platform.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using elearning_platform.Exceptions;

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

        public StarredSubject StarSubject(StarredSubject starredSubject)
        {
            var existingObj = _ctx.StarredSubjects.FirstOrDefault(e => e.SubjectId == starredSubject.SubjectId && e.UserId == starredSubject.UserId);
            if(existingObj == null)
            {   
                _ctx.StarredSubjects.Add(starredSubject);
                existingObj = starredSubject;
                _ctx.SaveChanges();
            }
            return existingObj;
        }

        public IEnumerable<StarredSubject> GetStarredSubjectsForUser(User user)
        {
            return _ctx.StarredSubjects.Include(e => e.Subject.StarredSubjects).Where(e => e.UserId == user.Uid).ToList();
            
        }

        public IEnumerable<Subject> GetAllSubjects()
        {
            return _ctx.Subjects;
        }

        public IEnumerable<EducationLevel> GetEducationLevels()
        {
            return _ctx.EducationLevels;
        }

        public SubjectRequest CreateSubjectRequest(SubjectRequest request)
        {
            _ctx.SubjectRequests.Add(request);
            _ctx.SaveChanges();
            return request;
        }

        public IEnumerable<SubjectRequest> GetAllSubjectRequests()
        {
            return _ctx.SubjectRequests.ToList();
        }

        public SubjectRequest SaveSubjectRequest(SubjectRequest request)
        {
            var entity = _ctx.SubjectRequests.FirstOrDefault(e => e.SubjectRequestId == request.SubjectRequestId);
            if (entity == null)
            {
                return null;
            }
            _ctx.Entry(entity).CurrentValues.SetValues(request);
            _ctx.SaveChanges();
            return request;
        }

        public Subject SaveSubject(Subject subject)
        {
            var entity = _ctx.Subjects.FirstOrDefault(e => e.SubjectId == subject.SubjectId);
            if (entity == null)
            {
                return null;
            }
            _ctx.Entry(entity).CurrentValues.SetValues(entity);
            _ctx.SaveChanges();
            return entity;   
        }

        public EducationLevel AddEducationLevel(EducationLevel level)
        {
            _ctx.EducationLevels.Add(level);
            _ctx.SaveChanges();
            return level;
        }

        public bool RemoveStarredSubject(StarredSubject starredSubject)
        {
            var entity = _ctx.StarredSubjects.FirstOrDefault(e => e.SubjectId == starredSubject.SubjectId && e.UserId == starredSubject.UserId);
            if(entity == null)
            {
                throw new BadRequestException("");
            }
            _ctx.StarredSubjects.Remove(entity);
            return _ctx.SaveChanges() > -1;
        }
    }
}