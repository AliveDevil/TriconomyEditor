using System.Collections.Generic;

namespace RuleSet
{
    public class Upgrade : SerializedObject
    {
        public List<Condition> Conditions { get; set; } = new List<Condition>();

        public List<Effect> Effects { get; set; } = new List<Effect>();

        public int Level { get; set; }
    }
}
