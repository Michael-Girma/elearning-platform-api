using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elearning_platform.Models
{
    public class User : BaseEntity
    {
        [Key]
        public Guid Uid { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        [Required]
        public string Password { get; set; }

        public bool EmailVerified { get; set; }

        public bool Banned { get; set; }

        public string? Location { get; set; }

        public string? ProfileImage { get; set; }

        public virtual ICollection<UserClaim> Claims { get; set; }

        public virtual PaymentAccountDetail? PaymentAccountDetail { get; set; }

        public virtual ICollection<UserChat> Chats { get; set; }
    }
}