using elearning_platform.DTO;
using elearning_platform.Models;
using YenePaySdk;

namespace elearning_platform.Services
{
    public interface ISessionService
    {
        TutorRequest? CreateTutorRequest(Student student, CreateTutorRequestDTO requestDTO);
        TutorRequest UpdateTutorRequest(Guid id, User user, UpdateTutorRequestDTO updateTutorRequestDTO);

        TutorRequest SetupAcceptedTutorRequest(Guid id, Tutor tutor);

        SessionPaymentLink GenerateLinkForBooking(Guid tutorRequestId, Student student, CreatePaymentLinkDTO paymentLinkDTO);

        Task<Session> BookSession(PaymentDetail ipn);
    }
}