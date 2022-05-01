using System.ComponentModel.DataAnnotations;

namespace elearning_platform.DTO
{
    public class SignupDTO
    {
        [Key]
        public Guid Uid { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [EmailAddress]
        private string _emailAddress { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-‌​]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$", ErrorMessage = "Email is not valid")]
        public string Email
        {
            get { return _emailAddress; }
            set
            {
                _emailAddress = value.Trim();
            }
        }
        [Required]
        [StringLength(10)]
        public string PhoneNumber { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
}