using elearning_platform.Models;

namespace elearning_platform.Repo
{
    public interface IStudentRepo
    {
        IEnumerable<Student> GetStudents(bool includeRelations = false);

        Student? GetStudentById(Guid studentId, bool includeRelations = false);

        Student? GetStudentByUid(Guid uid, bool includeRelations = false);
        Student CreateStudent(Student student);
        bool SaveChanges();
    }
}