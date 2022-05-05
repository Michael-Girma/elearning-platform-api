using elearning_platform.Attributes.Validation;
using static elearning_platform.Models.TutorRequest;

namespace elearning_platform.DTO
{
    public class StudentUpdateTutorRequest : UpdateTutorRequestDTO
    {

        [TutorRequestStatus(RequestStatusValues.Cancelled, RequestStatusValues.AwaitingTutor)]
        public override string Status { get; set; }
    }
}