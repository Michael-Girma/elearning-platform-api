namespace elearning_platform.Exceptions
{
    public class BadRequestException : BaseException
    {
        public BadRequestException(string description) : base(description) { }
        public BadRequestException(string description, string code) : base(description, code) { }
    }
}