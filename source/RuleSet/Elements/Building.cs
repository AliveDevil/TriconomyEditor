using System.Collections.Generic;

namespace RuleSet.Elements
{
    public class Building : Element
    {
        public List<Event> Events
        {
            get;
        } = new List<Event>();

        public List<Upgrade> Upgrades
        {
            get;
        } = new List<Upgrade>();

        public int Variants { get; set; } = 0;
    }
}
