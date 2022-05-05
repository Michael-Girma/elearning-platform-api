using elearning_platform.DTO;
using elearning_platform.Models;

namespace elearning_platform.Services
{
    public interface ISessionService
    {
        TutorRequest? CreateTutorRequest(Student student, CreateTutorRequestDTO requestDTO);
        TutorRequest UpdateTutorRequest(Guid id, User user, UpdateTutorRequestDTO updateTutorRequestDTO);

    }
}