using elearning_platform.DTO;
using elearning_platform.Models;

namespace elearning_platform.Services
{
    public interface IOnboardingService
    {
        User SignupUser(SignupDTO signupDTO);
        User setUserBan(Guid userId, bool banned);
        Tutor SignupTutor(TutorSignupDTO signupDTO);
        Student SignupStudent(StudentSignupDTO signupDTO);
        IEnumerable<User> GetAllUsers();
        User UpdateUserDetails(Guid userId, UpdateUserDetailsDTO user);
    }
}