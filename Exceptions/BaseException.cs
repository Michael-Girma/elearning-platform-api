namespace elearning_platform.Exceptions
{
    public class BaseException : Exception
    {
        public string StatusCode = "";
        public string StatusDescription = "";

        public BaseException(string description) : base(description)
        {
            StatusDescription = description;
        }

        public BaseException(string description, string code) : base(description)
        {
            StatusCode = code;
            StatusDescription = description;
        }
    }
}