using elearning_platform.Models;

namespace elearning_platform.Repo
{
    public interface ITutorRepo
    {
        Tutor? GetTutorById(Guid tutorId);

        Tutor? GetTutorByUid(Guid uid);

        Tutor CreateTutor(Tutor tutor);
    }
}