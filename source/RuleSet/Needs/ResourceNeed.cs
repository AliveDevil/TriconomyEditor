using de.alivedevil.Attributes;
using RuleSet.Elements;

namespace RuleSet.Needs
{
    public class ResourceNeed : Need
    {
        public int Amount { get; set; }

        [KeepReference]
        public Resource Resource { get; set; }
    }
}
