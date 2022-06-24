using AutoMapper;
using elearning_platform.DTO;
using elearning_platform.Models;

namespace elearning_platform.Profiles
{
    public class TutorProfile : Profile
    {
        public TutorProfile()
        {
            CreateMap<Tutor, ReadTutorDTO>();
            CreateMap<Tutor, ReadTutorDetailsDTO>();
            // CreateMap<>();
        }
    }
}