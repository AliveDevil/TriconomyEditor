using System;
using System.Collections.Generic;
using ProtoBuf;
using RuleSet.Needs;

namespace RuleSet
{
    [Serializable]
    public class Need
    {
        public List<Condition> Conditions { get; } = new List<Condition>();
    }
}
