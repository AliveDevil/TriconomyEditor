namespace RuleSet.Conditions
{
    public class ElementNearByCondition : Condition
    {
        public int Distance
        {
            get; set;
        }

        public Element Element
        {
            get; set;
        }
    }
}
