using System;

namespace RuleEngineCore.Annotaion
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ActionAttribute : Attribute
    {
        public int Order { get; set; } = 1;
    }
}