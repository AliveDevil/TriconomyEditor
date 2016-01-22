using de.alivedevil.Attributes;
using RuleSet.Elements;

namespace RuleSet.Effects
{
    public class ProduceResourceEffect : Effect
    {
        public int Amount { get; set; }

        public int Delay { get; set; }

        [KeepReference]
        public Resource Resource { get; set; }
    }
}
