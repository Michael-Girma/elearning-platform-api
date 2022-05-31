using elearning_platform.Models;

namespace elearning_platform.Services
{
    public interface ICurrentUserService
    {
        User? User { get; set; }
        Student? GetStudent();
        Tutor? GetTutor();

        Admin? GetAdmin();

        bool isAdmin { get; }

        void SetUser(Guid uid);
    }
}