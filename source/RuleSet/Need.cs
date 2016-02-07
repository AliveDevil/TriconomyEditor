using System;
using System.Collections.Generic;

namespace RuleSet
{
    [Serializable]
    public abstract class Need : SerializedObject
    {
        public List<Condition> Conditions { get; } = new List<Condition>();
    }
}
