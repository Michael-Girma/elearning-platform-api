using System.ComponentModel.DataAnnotations;
using elearning_platform.Models;

namespace elearning_platform.DTO
{
    public class ReadUserDTO : BaseEntityDTO
    {
        [Key]
        public Guid Uid { get; set; }

        [Required]
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public bool emailVerified { get; set; }

        public bool banned { get; set; }

        public string Location { get; set; }

        public string? ProfileImage { get; set; }

        public ReadPaymentAccountDetailDTO PaymentAccountDetail { get; set; }
    }
}
