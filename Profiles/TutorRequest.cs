using AutoMapper;
using elearning_platform.DTO;
using elearning_platform.Models;

namespace elearning_platform.Profiles
{
    public class TutorRequestProfile : Profile
    {
        public TutorRequestProfile()
        {
            CreateMap<CreateTutorRequestDTO, TutorRequest>();
            CreateMap<TutorRequest, ReadTutorRequestDTO>();
            CreateMap<UpdateTutorRequestDTO, TutorRequest>();
        }
    }
}