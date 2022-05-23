using System.ComponentModel.DataAnnotations;

namespace elearning_platform.DTO
{
    public class CreateMessageAttachmentDTO
    {
        [Required]
        public Guid FileId { get; set; }

        
    }
}