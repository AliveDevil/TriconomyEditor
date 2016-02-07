using System.Collections.Generic;
using RuleSet.Elements;

namespace RuleSet
{
    public class Research
    {
        public List<Condition> Conditions { get; } = new List<Condition>();

        public List<ResourcePart> Costs { get; } = new List<ResourcePart>();

        public string Name
        {
            get; set;
        }

        public int Time
        {
            get; set;
        }
    }
}
