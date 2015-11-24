using RuleSet.Elements;

namespace RuleSet.Effects
{
    public class WorkplaceEffect : Effect
    {
        public int Amount { get; set; }

        public Job Job { get; set; }
    }
}
