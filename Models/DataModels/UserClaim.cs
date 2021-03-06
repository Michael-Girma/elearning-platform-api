using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elearning_platform.Models
{
    public class UserClaim
    {
        [Key]
        public int UserClaimId { get; set; }

        public string Claim { get; set; }

        public string Value { get; set; }

        [ForeignKey("User")]
        public int Uid { get; set; }

        public User User { get; set; }
    }
}