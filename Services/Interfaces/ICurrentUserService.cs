using elearning_platform.Models;

namespace elearning_platform.Services
{
    public interface ICurrentUserService
    {
        User? User { get; set; }
        Student? GetStudent();
        Tutor? GetTutor();

        bool isAdmin { get; }

        void SetUser(Guid uid);
    }
}