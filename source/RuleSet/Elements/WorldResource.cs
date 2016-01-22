using de.alivedevil.Attributes;

namespace RuleSet.Elements
{
    public class WorldResource : Element
    {
        public int Amount { get; set; }

        [KeepReference]
        public Resource Resource { get; set; }

        public int SpawnChance { get; set; }

        public int Variants { get; set; } = 0;
    }
}
