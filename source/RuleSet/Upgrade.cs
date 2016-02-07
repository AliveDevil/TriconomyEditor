using System.Collections.Generic;
using RuleSet.Elements;

namespace RuleSet
{
    public class Upgrade
    {
        public List<Condition> Conditions { get; } = new List<Condition>();

        public List<ResourcePart> Costs { get; } = new List<ResourcePart>();

        public List<Effect> Effects { get; } = new List<Effect>();

        public int Level
        {
            get; set;
        }
    }
}
