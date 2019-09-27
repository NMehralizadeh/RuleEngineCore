using System.Collections.Generic;
using RuleEngineCore.Annotaion;
using System.Linq;
using System.Reflection;
using RuleEngineCore.RuleEngine.Exceptions;
using System;

namespace RuleEngineCore.RuleEngine
{
    public class Rule
    {
        private readonly List<string> _errorMessages;
        private readonly object _object;

        public Rule(object Object)
        {
            _object = Object;
            _errorMessages = new List<string>();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0019:Use pattern matching", Justification = "<Pending>")]
        public bool Evaluate(Facts facts)
        {
            return _object
                .GetType()
                .GetMethods()
                .Where(x => x.GetCustomAttributes(typeof(ConditionAttribute), false).Length > 0)
                .Select(x =>
                {
                    List<string> factArgs = null;
                    var exceptionMessage = string.Empty;
                    foreach (var attr in x.GetCustomAttributes(true))
                    {
                        var conditionAttribute = attr as ConditionAttribute;
                        if (conditionAttribute == null) continue;

                        factArgs = conditionAttribute.FactArgs;
                        exceptionMessage = conditionAttribute.ExceptionMessage;
                        break;
                    }
                    var result = (bool)x.Invoke(_object, factArgs?.Select(facts.Get).ToArray());
                    if (!result)
                    {
                        _errorMessages.Add(exceptionMessage);
                    }
                    return result;
                })
                .All(x => x);
        }

        /// <summary>
        /// Execute action by facts
        /// </summary>
        /// <param name="facts"></param>
        /// <exception cref="RuleException"></exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0019:Use pattern matching", Justification = "<Pending>")]
        public void Execute(Facts facts)
        {
            if (Evaluate(facts))
            {
                ExecuteActionsByPriority();

                var ruleAttribute = (_object.GetType().GetCustomAttribute(typeof(RuleAttribute)) as RuleAttribute);
                if (ruleAttribute.NextRule != null)
                {
                    ExecuteNextRule(facts);
                }
            }
            else
            {
                RuleException ruleException = null;
                foreach (var errorMessage in _errorMessages)
                {
                    ruleException = new RuleException(errorMessage, ruleException);
                }
                throw new RuleException(_object.GetType().FullName, ruleException);
            }
        }

        private void ExecuteActionsByPriority()
        {
            MethodInfo[] actions = _object
                            .GetType()
                            .GetMethods()
                            .Where(x => x.GetCustomAttributes(typeof(ActionAttribute), false).Length > 0)
                            .OrderBy(x =>
                            {
                                var order = int.MaxValue;
                                foreach (var attr in x.GetCustomAttributes(true))
                                {
                                    var actionAttribute = attr as ActionAttribute;
                                    if (actionAttribute == null) continue;

                                    order = actionAttribute.Order;
                                    break;
                                }
                                return order;
                            })
                            .ToArray();

            foreach (var action in actions)
            {
                action.Invoke(_object, new object[] { });
            }
        }

        private void ExecuteNextRule(Facts facts)
        {
            var ruleAttribute = (_object.GetType().GetCustomAttribute(typeof(RuleAttribute)) as RuleAttribute);
            var nextRuleType = ruleAttribute.NextRule;
            if (nextRuleType.GetCustomAttribute(typeof(RuleAttribute)) != null)
            {
                var nextRuleInstance = Activator.CreateInstance(nextRuleType);
                var nextRule = new Rule(nextRuleInstance);
                nextRule.Execute(facts);
            }
        }
    }
}
