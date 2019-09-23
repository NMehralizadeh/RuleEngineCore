using System;
using System.Collections.Generic;
using System.Linq;

namespace RuleEngineCore.Annotaion
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ConditionAttribute : Attribute
    {
        public ConditionAttribute(string factArgs,string exceptionMessage)
        {
            FactArgs = string.IsNullOrEmpty(factArgs) ? new List<string>(0): factArgs.Split(',').ToList();
            ExceptionMessage = exceptionMessage;
        }
        public List<string> FactArgs{ get; }
        public string ExceptionMessage { get; }
    }
}
