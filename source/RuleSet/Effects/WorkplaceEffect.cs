using de.alivedevil.Attributes;
using RuleSet.Elements;

namespace RuleSet.Effects
{
    public class WorkplaceEffect : Effect
    {
        [KeepReference]
        public Job Job { get; set; }
    }
}
