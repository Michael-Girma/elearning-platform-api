using AutoMapper;
using elearning_platform.DTO;
using elearning_platform.Models;

namespace elearning_platform.Profiles
{
    public class AssessmentProfile: Profile
    {
        public AssessmentProfile()
        {
            CreateMap<CreateAssessmentDTO, Assessment>();
            CreateMap<CreateAssessmentQuestionDTO, AssessmentQuestion>();
            CreateMap<CreateQuestionChoiceDTO, QuestionChoice>();
        }
    }
}