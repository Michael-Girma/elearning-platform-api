using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elearning_platform.Models
{
    public class UserClaim : BaseEntity
    {
        [Key]
        public Guid UserClaimId { get; set; }

        public string Claim { get; set; }

        public string Value { get; set; }

        [ForeignKey("User")]
        public Guid Uid { get; set; }

        public virtual User User { get; set; }
    }
}