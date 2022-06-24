using AutoMapper;
using elearning_platform.DTO;
using elearning_platform.Models;

namespace elearning_platform.Profiles
{
    public class SessionProfile : Profile
    {
        public SessionProfile()
        {
            CreateMap<Tutor, ReadTutorDTO>();
            CreateMap<Session, ReadSessionDTO>();
            CreateMap<SessionOrder, ReadSessionOrderDTO>();
            CreateMap<SessionPaymentLink, ReadSessionPaymentLinkDTO>();
            CreateMap<SessionFeedback, ReadSessionFeedbackDTO>();
            CreateMap<CreateSessionFeedbackDTO, SessionFeedback>();
            // CreateMap<ICollection<Session>, ICollection<ReadSessionDTO>();
        }
    }
}