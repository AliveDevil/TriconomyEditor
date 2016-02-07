using RuleSet.Elements;

namespace RuleSet.Conditions
{
    public class ExistingBuildingCondition : Condition
    {
        public Building Building
        {
            get; set;
        }
    }
}
