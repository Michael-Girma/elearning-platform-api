using elearning_platform.DTO;

namespace elearning_platform.Services
{
    public interface IStatService
    {
        ReadStudentActivityDTO GetStudentActivity(Guid id);
    }
}