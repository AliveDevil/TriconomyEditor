using de.alivedevil.Attributes;
using RuleSet.Elements;

namespace RuleSet.Conditions
{
    public class ResearchCondition : Condition
    {
        [KeepReference]
        public Research Research
        {
            get; set;
        }
    }
}
