using System;

namespace RuleEngineCore.RuleEngine.Exceptions
{
    [Serializable]
    public class RuleException : Exception
    {
        public RuleException(){}
        public RuleException(string message) :
            base(message){}
        public RuleException(string message,Exception innerException) :
            base(message, innerException){}
    }
}
