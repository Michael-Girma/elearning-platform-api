using AutoMapper;
using elearning_platform.DTO;
using elearning_platform.Models;

namespace elearning_platform.Profiles
{
    public class TaughtSubjectProfile : Profile
    {
        public TaughtSubjectProfile()
        {
            CreateMap<CreateTaughtSubjectDTO, TaughtSubject>();
            CreateMap<TaughtSubject, ReadTaughtSubjectDTO>();
        }
    }
}