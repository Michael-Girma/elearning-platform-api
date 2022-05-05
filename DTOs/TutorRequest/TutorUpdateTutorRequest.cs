using elearning_platform.Attributes.Validation;
using static elearning_platform.Models.TutorRequest;

namespace elearning_platform.DTO
{
    public class TutorUpdateTutorRequest : UpdateTutorRequestDTO
    {

        [TutorRequestStatus(RequestStatusValues.AwaitingStudent, RequestStatusValues.Rejected)]
        public override string Status { get; set; }
    }
}