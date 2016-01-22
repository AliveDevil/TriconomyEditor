using System.Collections.Generic;

namespace RuleSet
{
    public abstract class Need : SerializedObject
    {
        public List<Condition> Conditions { get; } = new List<Condition>();
    }
}
