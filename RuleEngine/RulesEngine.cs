namespace RuleEngineCore.RuleEngine
{
    public class RulesEngine
    {
        public virtual void Fire(Rule rule, Facts facts)
        {
            rule.Execute(facts);
        }
    }
}
