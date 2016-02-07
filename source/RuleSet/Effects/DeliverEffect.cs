using de.alivedevil.Attributes;
using System;
using RuleSet.Elements;
using ProtoBuf;

namespace RuleSet.Effects
{
    [Serializable]
    public class DeliverEffect : Effect
    {
        [KeepReference]
        public Building PreferredBuilding
        {
            get; set;
        }

        public int Priority
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
