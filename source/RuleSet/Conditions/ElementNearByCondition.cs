using de.alivedevil.Attributes;

namespace RuleSet.Conditions
{
    public class ElementNearByCondition : Condition
    {
        public int Distance { get; set; }

        [KeepReference]
        public Element Element { get; set; }
    }
}
