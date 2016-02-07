using de.alivedevil.Attributes;
using System;
using ProtoBuf;

namespace RuleSet.Conditions
{
    [Serializable]
    public class ResearchCondition : Condition
    {
        [KeepReference]
        public Research Research
        {
            get; set;
        }
    }
}
