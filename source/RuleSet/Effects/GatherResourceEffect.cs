using RuleSet.Elements;

namespace RuleSet.Effects
{
    public class GatherResourceEffect : Effect
    {
        public int Radius { get; set; }

        public Resource Resource { get; set; }
    }
}
