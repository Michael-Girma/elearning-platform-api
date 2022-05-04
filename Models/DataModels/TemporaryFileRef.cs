namespace elearning_platform.Models
{
    public class TemporaryFileRef : BaseEntity
    {
        public Guid FileId { get; set; }
        public string ContentHash { get; set; }
        public string TemporaryDownloadLink { get; set; }
    }
}