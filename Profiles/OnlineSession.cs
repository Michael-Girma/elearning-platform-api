using AutoMapper;
using elearning_platform.DTO;
using elearning_platform.Models;

namespace elearning_platform.Profiles
{
    public class OnlineSessionProfile : Profile
    {
        public OnlineSessionProfile()
        {
            CreateMap<OnlineSession, ReadOnlineSessionDTO>();
        }
    }
}