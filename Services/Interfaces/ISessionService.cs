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

        SessionPaymentLink GenerateLinkForSession(Guid sessionId, Student student, CreatePaymentLinkDTO paymentLinkDTO);

        Task<IEnumerable<ReadEnquiryInsightDTO>> GetAllEnquiryInsights(Guid studentId);

        IEnumerable<Session> GetAllSessionsForUser(Guid uId);

        string GenerateVideoChatLink(Guid sessionId);

        SessionFeedback LeaveFeedback(Student student, Guid sessionId, CreateSessionFeedbackDTO feedbackDTO);

        IEnumerable<SessionFeedback> GetFeedbacksOfStudent(Guid studentId);
        IEnumerable<SessionFeedback> GetFeedbacksOfTutor(Guid tutorId);

        Session UpdateStudentNotes(Student student, Guid sessionId, UpdateStudentNotesDTO notesDTO);

        Session UpdateRecommendations(Tutor tutor, Guid sessionId, UpdateRecommendationsDTO recommendationsDTO);

        Resource UploadResource(Tutor tutor, Guid sessionId, CreateResourceDTO resourceDTO);

        bool RemoveResource(Tutor tutor, Guid resourceId);

        Assessment AddAssessment(Tutor tutor, Guid sessionId, CreateAssessmentDTO assessmentDTO);

        bool RemoveAssessment(Tutor tutor, Guid assessmentId);

        IEnumerable<SessionFeedback> GetAllReports();

        SessionFeedback MarkAsAddressed(Guid feedbackId);
    }
}