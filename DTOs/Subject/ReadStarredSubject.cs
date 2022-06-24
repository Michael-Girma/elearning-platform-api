namespace elearning_platform.DTO
{
    public class ReadStarredSubject : BaseEntityDTO
    {
        public Guid SubjectId {get; set;}

        public Guid UserId {get;set;}

        public ReadUserDTO User { get; set; }

        public ReadSubjectDTO Subject {get; set;} 
    }
}