using RuleSet.Elements;

namespace RuleSet.Effects
{
    public class ProduceResourceEffect : Effect
    {
        public int Amount { get; set; }

        public int Delay { get; set; }

        public Resource Resource { get; set; }
    }
}
