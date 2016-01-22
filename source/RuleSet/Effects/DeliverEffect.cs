using de.alivedevil.Attributes;
using RuleSet.Elements;

namespace RuleSet.Effects
{
    public class DeliverEffect : Effect
    {
        [KeepReference]
        public Building PreferredBuilding { get; set; }

        public int Priority { get; set; }

        [KeepReference]
        public Resource Resource { get; set; }
    }
}
