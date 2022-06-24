using System.ComponentModel.DataAnnotations;
using elearning_platform.Models;

namespace elearning_platform.DTO
{
    public class CreateTaughtSubjectDTO
    {

        [Required]
        public Guid SubjectId { get; set; }

        [Required]
        public float PricePerHour { get; set; }

        [Required]
        public bool TaughtOnline { get; set; }

        [Required]
        public string TopicsCovered { get; set; }

        [Required]
        public bool TaughtInPerson { get; set; }

        [Required]
        public int PreferredSessionCount { get; set; }

        [Required]
        public int PreferredSessionLength { get; set;}

        public IEnumerable<CreateLessonDocumentDTO>? LessonDocuments { get; set; }
    }
}