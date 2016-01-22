using System.Collections.Generic;
using RuleSet.Elements;

namespace RuleSet
{
    public class Upgrade : SerializedObject
    {
        public List<Condition> Conditions { get; set; } = new List<Condition>();

        public List<ResourcePart> Costs { get; } = new List<ResourcePart>();

        public List<Effect> Effects { get; set; } = new List<Effect>();

        public int Level { get; set; }
    }
}
