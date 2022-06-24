namespace elearning_platform.DTO
{
    public class ReadPaymentAccountDetailDTO
    {
        public Guid Id { get; set; }

        public string? YenePaySellerCode { get; set; }

        public Guid Uid { get; set; }
        public virtual ReadUserDTO User { get; set; }
    }
}