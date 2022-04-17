using elearning_platform.Models;

namespace elearning_platform.Services
{
    public interface ICurrentUserService
    {
        User User { get; set; }
        Student? GetStudent();

        bool isAdmin { get; }

        void SetUser(int uid);
    }
}