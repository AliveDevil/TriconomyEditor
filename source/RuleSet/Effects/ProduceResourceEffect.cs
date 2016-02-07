using System;
using de.alivedevil.Attributes;
using ProtoBuf;
using RuleSet.Elements;

namespace RuleSet.Effects
{
    [Serializable]
    public class ProduceResourceEffect : Effect
    {
        public int Amount
        {
            get; set;
        }

        public bool CheatModeOnly
        {
            get; set;
        }

        public int Delay
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
