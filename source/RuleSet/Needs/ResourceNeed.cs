using System;
using de.alivedevil.Attributes;
using ProtoBuf;
using RuleSet.Elements;

namespace RuleSet.Needs
{
    [Serializable]
    public class ResourceNeed : Need
    {
        public int Amount
        {
            get; set;
        }

        [KeepReference]
        public Resource Resource
        {
            get; set;
        }
    }
}
