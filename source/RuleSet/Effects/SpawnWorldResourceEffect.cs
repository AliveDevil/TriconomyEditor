using System;
using de.alivedevil.Attributes;
using ProtoBuf;
using RuleSet.Elements;

namespace RuleSet.Effects
{
    [Serializable]
    public class SpawnWorldResourceEffect : Effect
    {
        public int Amount
        {
            get; set;
        }

        public int Delay
        {
            get; set;
        }

        public int Radius
        {
            get; set;
        }

        [KeepReference]
        public WorldResource WorldResource
        {
            get; set;
        }
    }
}
