using elearning_platform.DTO;
using elearning_platform.Models;

namespace elearning_platform.Services
{
    public interface ISubjectService
    {
        Subject CreateSubject(User user, CreateSubjectDTO createSubjectDTO);
    }
}