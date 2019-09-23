using RuleEngineCore.RuleEngine.Exceptions;
using System.Collections.Generic;

namespace RuleEngineCore.RuleEngine
{
    public class Facts
    {
        public enum PutType
        {
            Safe,
            NotSafe
        }

        private readonly Dictionary<string, object> _collector = new Dictionary<string, object>();
        public object Get(string action)
        {
            return HasFact(action) ? _collector[action] : null;
        }

        public bool HasFact(string action)
        {
            return _collector.ContainsKey(action);
        }

        public void Put(string action, object value, PutType putType = PutType.NotSafe)
        {
            if (HasFact(action))
            {
                if (putType == PutType.NotSafe)
                {
                    throw new FactsException("Duplicated action name.");
                }

                _collector[action] = value;
            }
            _collector.Add(action, value);
        }
    }
}
