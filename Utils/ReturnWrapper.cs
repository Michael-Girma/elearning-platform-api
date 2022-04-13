namespace elearning_platform.Utils
{
    public class DetailedReturn<T, G>
    {
        public T Status { get; set; }
        public G? Value { get; set; }

        public DetailedReturn(T status, G value)
        {
            Status = status;
            Value = value;
        }

        public DetailedReturn(T status)
        {
            Status = status;
        }
    }
}