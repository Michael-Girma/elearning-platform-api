
using Microsoft.EntityFrameworkCore;
using elearning_platform.Data;
using elearning_platform.Models;

namespace elearning_platform.Repo
{
    public class StudentRepo : IStudentRepo
    {
        private readonly AppDbContext _ctx;

        public StudentRepo(AppDbContext context)
        {
            _ctx = context;
        }

        public Student? GetStudentById(int studentId, bool includeRelations = false)
        {
            throw new NotImplementedException();
        }

        public Student? GetStudentByUid(int uid, bool includeRelations = false)
        {
            var students = includeRelations ?
                _ctx.Students.Include(s => s.User).Include(s => s.EducationLevel)
                : _ctx.Students.AsQueryable();

            return students.SingleOrDefault(s => s.Uid == uid);
        }

        public IEnumerable<Student> GetStudents(bool includeRelations = false)
        {
            throw new NotImplementedException();
        }

        public Student CreateStudent(Student student)
        {
            _ctx.Students.Add(student);
            return student;
        }

        public bool SaveChanges()
        {
            return _ctx.SaveChanges() > 0;
        }
    }
}