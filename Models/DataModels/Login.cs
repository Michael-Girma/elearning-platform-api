using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elearning_platform.Models
{
    public class Login: BaseEntity
    {
        [Key]
        public Guid LoginId { get; set; }

        [ForeignKey("User")]
        public Guid UId { get; set; }
    }
}