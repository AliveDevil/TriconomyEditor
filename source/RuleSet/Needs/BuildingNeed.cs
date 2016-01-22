using de.alivedevil.Attributes;
using RuleSet.Elements;

namespace RuleSet.Needs
{
    public class BuildingNeed : Need
    {
        [KeepReference]
        public Building Building { get; set; }

        public int Radius { get; set; }
    }
}
