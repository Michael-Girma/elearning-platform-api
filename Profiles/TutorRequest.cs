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
            .AfterMap((src, dest) => dest.TaughtSubject.Tutor = null);
            // ProxyUtil.GetUnproxiedInstance()
            // CreateMap().ProjectTo<ReadTutorRequestDTO>(new[] { TutorRequest }.AsQueryable()).Single();
            // CreateMaTO, TutorRequest>p<TutorRequest, ReadTutorRequestDTO>();

        }
    }
}