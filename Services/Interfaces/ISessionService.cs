using elearning_platform.DTO;
using elearning_platform.Models;

namespace elearning_platform.Services
{
    public interface ISessionService
    {
        TutorRequest? CreateTutorRequest(Student student, CreateTutorRequestDTO requestDTO);
        TutorRequest UpdateTutorRequest(Guid id, User user, UpdateTutorRequestDTO updateTutorRequestDTO);

        TutorRequest SetupAcceptedTutorRequest(Guid id, Tutor tutor);

        PaymentLink GenerateLinkForBooking(Guid tutorRequestId, Student student, CreatePaymentLinkDTO paymentLinkDTO);

    }
}