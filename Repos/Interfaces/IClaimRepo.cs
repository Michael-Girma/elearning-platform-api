using elearning_platform.Models;

namespace elearning_platform.Repo
{
    public interface IClaimRepo
    {
        Student AddClaimForStudent(Student student);
    }
}