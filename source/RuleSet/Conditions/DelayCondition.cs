using System;

namespace RuleSet.Conditions
{
    public class DelayCondition : Condition
    {
        public TimeSpan Delay
        {
            get; set;
        }
    }
}
