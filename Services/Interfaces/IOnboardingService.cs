using elearning_platform.DTO;
using elearning_platform.Models;

namespace elearning_platform.Services
{
    public interface IOnboardingService
    {
        User SignupUser(SignupDTO signupDTO);
        Tutor SignupTutor(SignupDTO signupDTO);
    }
}