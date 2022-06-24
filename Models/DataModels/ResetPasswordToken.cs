using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elearning_platform.Models
{
    public class ResetPasswordToken: BaseEntity
    {
        [Key]
        public Guid ResetPasswordTokenId { get; set; }
        public string Token { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public virtual User User { get; set; }

        public bool Used { get; set; }

        public DateTime ExpiresAt{ get; set; } = DateTime.UtcNow.AddMinutes(10);
    }
}