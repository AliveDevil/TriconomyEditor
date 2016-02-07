using RuleSet.Elements;

namespace RuleSet.Needs
{
    public class ResourceNeed : Need
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
