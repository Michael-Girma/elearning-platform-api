using elearning_platform.Data;
using elearning_platform.Models;
using elearning_platform.Repo;

namespace elearning_platform.Services
{
    public class TutorRequestService : ITutorRequestService
    {
        private readonly AppDbContext _ctx;
        private readonly ITutorRequestRepo _tutorRequestRepo;

        public TutorRequestService(AppDbContext ctx, ITutorRequestRepo tutorRequestRepo)
        {
            _ctx = ctx;
            _tutorRequestRepo = tutorRequestRepo;
        }

        public IEnumerable<TutorRequest> GetRequestsForStudent(Guid id)
        {
            return _tutorRequestRepo.GetTutorRequestsForStudent(id).ToList();
        }
    }
}