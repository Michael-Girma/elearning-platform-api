using AutoMapper;
using elearning_platform.DTO;
using elearning_platform.Models;

namespace elearning_platform.Profiles
{
    public class AdminProfile: Profile
    {
        public AdminProfile()
        {
            CreateMap<Admin, ReadAdminDTO>();
        }
    }
}