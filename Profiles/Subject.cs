using AutoMapper;
using elearning_platform.DTO;
using elearning_platform.Models;

namespace elearning_platform.Profiles
{
    public class SubjectProfile : Profile
    {
        public SubjectProfile()
        {
            CreateMap<CreateSubjectDTO, Subject>();
            CreateMap<Subject, ReadSubjectDTO>();
            CreateMap<StarredSubject, ReadStarredSubject>();
            CreateMap<SubjectRequest, ReadSubjectRequestDTO>();
            CreateMap<CreateSubjectRequestDTO, SubjectRequest>();
        }
    }
}