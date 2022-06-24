using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elearning_platform.Models
{
    public class SubjectRequest : BaseEntity
    {
        [Key]
        public Guid SubjectRequestId { get; set; }

        public string SubjectName { get; set; }

        [ForeignKey("Tutor")]
        public Guid RequestAuthorId { get; set; }

        public bool Approved { get; set; }
        public bool Addressed { get; set; }

        [ForeignKey("EducationLevel")]
        public Guid EducationLevelId { get; set;}

        public string Description { get; set; }

        public virtual EducationLevel EducationLevel { get; set; }
        public virtual Tutor Tutor { get; set; }
    }
}