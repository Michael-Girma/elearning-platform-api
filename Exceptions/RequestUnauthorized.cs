namespace elearning_platform.Exceptions
{
    public class RequestUnauthorizedException : BaseException
    {
        public RequestUnauthorizedException(string description) : base(description) { }
        // public RequestUnauthorizedExcetion(string description, string codes) : base(description, codes) { }
    }
}