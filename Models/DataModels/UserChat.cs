using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elearning_platform.Models 
{
    public class UserChat : BaseEntity
    {
        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [ForeignKey("Chat")]
        public Guid ChatId { get; set; }

        public virtual User User { get; set; }
        public virtual Chat Chat { get; set; }
    } 
}