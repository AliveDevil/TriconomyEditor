using de.alivedevil.Attributes;
using System;
using RuleSet.Elements;
using ProtoBuf;

namespace RuleSet.Effects
{
    [Serializable]
    public class GatherResourceEffect : Effect
    {
        public int Radius
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
