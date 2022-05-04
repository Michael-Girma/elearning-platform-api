using System.ComponentModel.DataAnnotations.Schema;

namespace elearning_platform.Models
{
    public class Admin : BaseEntity
    {
        public Guid AdminId { get; set; }

        [ForeignKey("User")]
        public Guid Uid { get; set; }

        public User User { get; set; }
    }
}