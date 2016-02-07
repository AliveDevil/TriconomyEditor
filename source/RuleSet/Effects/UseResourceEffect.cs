using RuleSet.Elements;

namespace RuleSet.Effects
{
    public class UseResourceEffect : Effect
    {
        public int Amount
        {
            get; set;
        }

        public Resource Resource
        {
            get; set;
        }
    }
}
