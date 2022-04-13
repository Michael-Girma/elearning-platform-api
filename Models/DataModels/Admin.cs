using System.ComponentModel.DataAnnotations.Schema;

namespace elearning_platform.Models
{
    public class Admin
    {
        public int AdminId { get; set; }

        [ForeignKey("User")]
        public int Uid { get; set; }

        public User User { get; set; }
    }
}