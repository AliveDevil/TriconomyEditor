using RuleSet.Elements;

namespace RuleSet.Effects
{
    public class DeliverEffect : Effect
    {
        public Building PreferredBuilding
        {
            get; set;
        }

        public int Priority
        {
            get; set;
        }

        public Resource Resource
        {
            get; set;
        }
    }
}
