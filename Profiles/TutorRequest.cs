using AutoMapper;
using elearning_platform.DTO;
using elearning_platform.Models;
using Castle.DynamicProxy;
namespace elearning_platform.Profiles
{
    public class TutorRequestProfile : Profile
    {
        public TutorRequestProfile()
        {
            CreateMap<CreateTutorRequestDTO, TutorRequest>();
            
            CreateMap<UpdateTutorRequestDTO, TutorRequest>();
            CreateMap<TutorRequest, ReadTutorRequestDTO>()
            .MaxDepth(1);
            

        }
    }
}