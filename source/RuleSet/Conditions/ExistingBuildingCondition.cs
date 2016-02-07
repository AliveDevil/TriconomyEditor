using System;
using ProtoBuf;
using RuleSet.Elements;

namespace RuleSet.Conditions
{
    [Serializable]
    public class ExistingBuildingCondition : Condition
    {
        public Building Building
        {
            get; set;
        }
    }
}
