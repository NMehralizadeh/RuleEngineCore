using System;

namespace RuleEngineCore.RuleEngine.Exceptions
{
    [Serializable]
    public class FactsException : Exception
    {
        public FactsException(){}
        public FactsException(string message) :
            base(message){}
        public FactsException(string message,Exception innerException) :
            base(message, innerException){}
    }
}
