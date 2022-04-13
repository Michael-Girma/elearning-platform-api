using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elearning_platform.Models
{
    public class Mfa
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int Uid { get; set; }

        public DateTime ExpiresAt { get; set; }

        public DateTime Iat { get; set; }

        public int PinCode { get; set; }
    }
}