using System;

namespace RuleEngineCore.Annotaion
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RuleAttribute : Attribute
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Type NextRule { get; set; }
    }
}
