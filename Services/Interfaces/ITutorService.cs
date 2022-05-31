using elearning_platform.DTO;

namespace elearning_platform.Services
{
    public interface ITutorService
    {
        ReadTutorDetailsDTO GetTutorDetails(Guid tutorId);
    }
}