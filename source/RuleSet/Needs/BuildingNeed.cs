using RuleSet.Elements;

namespace RuleSet.Needs
{
    public class BuildingNeed : Need
    {
        public Building Building
        {
            get; set;
        }

        public int Radius
        {
            get; set;
        }
    }
}
