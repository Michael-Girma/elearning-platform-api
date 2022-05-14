using System;

namespace elearning_platform.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    sealed public class HubAttribute : Attribute
    {
        public string route { get; set; }

        public HubAttribute(string _route)
        {
            route  = _route;
        }
    }
}