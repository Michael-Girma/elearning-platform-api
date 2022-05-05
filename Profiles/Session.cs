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
            // CreateMap<ICollection<Session>, ICollection<ReadSessionDTO>();
        }
    }
}