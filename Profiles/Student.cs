using AutoMapper;
using elearning_platform.DTO;
using elearning_platform.Models;

namespace elearning_platform.Profiles
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<StudentSignupDTO, Student>().ForMember(student => student.StudentId, opt => opt.Ignore());
        }
    }
}

