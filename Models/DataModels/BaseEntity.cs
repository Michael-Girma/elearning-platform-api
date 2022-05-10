namespace elearning_platform.Models
{
    public class BaseEntity
    {
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        public Type Unproxy()
        {
            var type = this.GetType();
            if(type.Namespace == "Castle.Proxies")
            {
                return type.BaseType;
            }
            return type;
        }
    }
}