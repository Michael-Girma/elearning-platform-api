namespace elearning_platform.Exceptions
{
    public class BadRequestException : Exception
    {
        public string StatusDescription;
        public string StatusCode = "";
        public BadRequestException(string description) : base(description)
        {
            StatusDescription = description;
        }

        public BadRequestException(string description, string code) : base(description)
        {
            StatusDescription = description;
            StatusCode = code;
        }

    }
}