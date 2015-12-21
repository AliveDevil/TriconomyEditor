using RuleSet.Elements;

namespace RuleSet.Effects
{
    public class SpawnWorldResourceEffect : Effect
    {
        public int Amount { get; set; }

        public int Delay { get; set; }

        public int Radius { get; set; }

        public WorldResource WorldResource { get; set; }
    }
}
