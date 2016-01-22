using de.alivedevil.Attributes;
using RuleSet.Elements;

namespace RuleSet.Effects
{
    public class UseResourceEffect : Effect
    {
        public int Amount { get; set; }

        [KeepReference]
        public Resource Resource { get; set; }
    }
}
