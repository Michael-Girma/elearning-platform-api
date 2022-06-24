using AutoMapper;
using elearning_platform.Models;
using elearning_platform.DTO;

namespace elearning_platform.Profiles
{
    public class ResourceProfile : Profile
    {
        public ResourceProfile()
        {
            CreateMap<CreateResourceDTO, Resource>();
            CreateMap<Resource, ReadResourceDTO>();
        }
    }
}