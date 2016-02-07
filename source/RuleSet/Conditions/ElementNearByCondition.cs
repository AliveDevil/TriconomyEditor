using System;
using de.alivedevil.Attributes;
using ProtoBuf;

namespace RuleSet.Conditions
{
    [Serializable]
    public class ElementNearByCondition : Condition
    {
        public int Distance
        {
            get; set;
        }

        [KeepReference]
        public Element Element
        {
            get; set;
        }
    }
}
