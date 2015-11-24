using System.Collections.Generic;
using Newtonsoft.Json;

namespace RuleSet.Elements
{
    public class Building : Element
    {
        public List<Condition> Conditions { get; set; } = new List<Condition>();

        public List<Effect> Effects { get; set; } = new List<Effect>();

        public List<Upgrade> Upgrades { get; set; } = new List<Upgrade>();

        public int Variants { get; set; } = 0;
    }
}
