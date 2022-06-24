using System.ComponentModel.DataAnnotations.Schema;

namespace elearning_platform.Models
{
    public class StarredSubject: BaseEntity
    {
        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [ForeignKey("Subject")]
        public Guid SubjectId { get; set; }

        public virtual Subject Subject { get; set; }
        public virtual User User { get; set; }
    }
}